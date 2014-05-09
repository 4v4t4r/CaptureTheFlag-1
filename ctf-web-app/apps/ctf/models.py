import logging
import datetime
from haystack.query import SearchQuerySet
from haystack.utils.geo import Point, D
from django.utils.translation import ugettext_lazy as _
from django.db import models
from model_utils import Choices
from apps.core.exceptions import AlreadyExistException, GameAlreadyStartedException, GameAlreadyFinishedException
from apps.core.models import PortalUser, GeoModel

logger = logging.getLogger("root")

MARKER_TYPES = Choices(
    (0, 'PLAYER', _('Player')),
    (1, 'PLAYER_WITH_RED_FLAG', _('Player with red flag')),
    (2, 'PLAYER_WITH_BLUE_FLAG', _('Player with blue flag')),

    (3, 'RED_FLAG', _('Red flag')),
    (4, 'BLUE_FLAG', _('Blue flag')),

    (5, 'RED_BASE', _('Red base')),
    (6, 'BLUE_BASE', _('Blue base')),

    (7, 'RED_BASE_WITH_FLAG', _('Red base with flag')),
    (8, 'BLUE_BASE_WITH_FLAG', _('Blue base with flag')),

    (9, 'FIRST_AID_KIT', _('First aid kit')),
    (10, 'PISTOL', _('Pistol')),
    (11, 'AMMO', _('Ammo')),
)


class Marker(GeoModel):
    type = models.IntegerField(choices=MARKER_TYPES, verbose_name=_("Type"))
    distance = models.FloatField(verbose_name=_("Distance"))
    url = models.URLField(verbose_name=_("URL"))

    def __unicode__(self):
        return "%s, %.2f m, url: %s" % (self.type, self.distance, self.url)

    class Meta:
        managed = False


class Item(GeoModel):
    ITEM_TYPES = MARKER_TYPES[3:]

    name = models.CharField(max_length=100, verbose_name=_("Name"))
    type = models.IntegerField(choices=ITEM_TYPES, verbose_name=_("Type"))
    value = models.FloatField(blank=True, null=True, verbose_name=_("Value"))
    description = models.TextField(blank=True, null=True, max_length=255, verbose_name=_("Description"))
    game = models.ForeignKey("Game", related_name="items", verbose_name=_("Game"))

    last_captured_by = models.ForeignKey(PortalUser, blank=True, null=True, verbose_name=_("Last captured by"))
    when_captured = models.DateTimeField(blank=True, null=True, verbose_name=_("When captured"))

    last_modified = models.DateTimeField(auto_now=True, verbose_name=_("Last modified"))
    created = models.DateTimeField(auto_now_add=True, verbose_name=_("Created"))

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"


class Game(GeoModel):
    GAME_STATUSES = Choices(
        (0, 'CREATED', _('Created')),
        (1, 'IN_PROGRESS', _('In progress')),
        (2, 'ON_HOLD', _('On hold')),
        (3, 'CANCELED', _('Canceled')),
        (4, 'FINISHED', _('Finished')),
    )

    GAME_TYPES = Choices(
        (0, 'BASED_ON_FRAGS', _('Based on frags')),
        (1, 'BASED_ON_TIME', _('Based on time')),
    )

    name = models.CharField(max_length=100, verbose_name=_("Name"))
    description = models.TextField(null=True, blank=True, max_length=255, verbose_name=_("Description"))

    radius = models.FloatField(verbose_name=_("Radius"))
    start_time = models.DateTimeField(verbose_name=_("Start time"))
    max_players = models.IntegerField(null=True, blank=True, verbose_name=_("Max players"))
    status = models.IntegerField(choices=GAME_STATUSES, default=GAME_STATUSES.CREATED, verbose_name=_("Status"))
    type = models.IntegerField(choices=GAME_TYPES, default=GAME_TYPES.BASED_ON_FRAGS, verbose_name=_("Type"))

    visibility_range = models.FloatField(default=200.00, verbose_name=_("Visibility range"))  # in meters
    action_range = models.FloatField(default=5.00, verbose_name=_("Action range"))  # in meters
    capture_time = models.IntegerField(default=10, verbose_name=_("Capture time"))  # in seconds

    players = models.ManyToManyField(PortalUser, verbose_name=_("Players"), related_name="joined_games")
    invited_users = models.ManyToManyField(PortalUser, verbose_name=_("Invited users"), related_name="pending_games")

    owner = models.ForeignKey(PortalUser, null=True, blank=True, verbose_name=_("Owner"))
    last_modified = models.DateTimeField(auto_now=True, verbose_name=_("Last modified"))
    created = models.DateTimeField(auto_now_add=True, verbose_name=_("Created"))

    @property
    def red_base(self):
        return self._get_item(MARKER_TYPES.RED_BASE)

    @property
    def blue_base(self):
        return self._get_item(MARKER_TYPES.BLUE_BASE)

    def add_player(self, user):
        if self.max_players and self.max_players == self.players.count():
            raise Exception("Max value of players exceeded")

        logger.debug("user: %s", user)

        is_user_already_exist = self.players.filter(pk=user.id).exists()
        logger.debug("is_user_already_exist: %s", is_user_already_exist)

        if is_user_already_exist:
            logger.info("User '%s' already joined into the game '%s: %s'", user.username, self.id, self.name)
            raise AlreadyExistException("User '%s' already joined into the game '%s: %s'" % (
                user.username, self.id, self.name))
        else:
            logger.info("Setting current game id: '%d' into the user: '%s'", self.id, user.username)
            logger.info("User: '%s' prev game: '%s'", user.username, user.current_game_id)

            if user.current_game_id is not None:
                game = Game.objects.get(pk=user.current_game_id)
                user.joined_games.remove(game)
                logger.info("User: '%s' was removed from prev game: '%s'", user.username, user.current_game_id)

            user.current_game_id = self.id
            user.save()

            logger.info("User '%s' is joining into the game '%s'...", user.username, self.name)
            self.players.add(user)
            self.save()

    def remove_player(self, user):
        if not self.players.filter(pk=user.id).exists():
            raise PortalUser.DoesNotExist("User '%s' doesn't exist in selected game '%s: %s'" % (
                user.username, self.id, self.name))

        self.players.remove(user)
        logger.info("User '%s' is removing from game '%s'...", user.username, self.name)
        self.save()

        logger.info("Cleaning current game id: '%d' from the user: '%s'", self.id, user.username)
        user.current_game_id = None
        user.save()

    def get_neighbours(self, user):
        location = user.location
        point = Point(location.lon, location.lat)
        max_dist = D(m=self.visibility_range)

        logger.debug("user: %s", user)
        logger.debug("location: %s", location)
        logger.debug("max_dist: %s", max_dist)

        sqs = SearchQuerySet().dwithin('location', point, max_dist).distance('location', point).filter(game=self.id)

        logger.debug("Query: %s", sqs.query)
        logger.debug("Query distance: %s", sqs.distance)

        return sqs

    def get_markers(self, user, context):
        from apps.ctf.api.serializers.common import ItemSerializer
        from apps.core.api.serializers import PortalUserSerializer

        neighbours = self.get_neighbours(user)

        markers = []

        for instance in neighbours:
            distance_in_meters = instance.distance.m if instance.distance is not None else None
            obj = instance.object
            logger.debug("object: %s, distance: %s m", obj, distance_in_meters)
            logger.debug("object type: %s", type(obj))

            if isinstance(obj, Item) or isinstance(obj, PortalUser):
                logger.debug("[PLAY]: user: %s, distance: '%f', action_range: '%f'", user, distance_in_meters, self.action_range)

                if isinstance(obj, Item):
                    serializer = ItemSerializer(obj, context=context)
                    json_data = serializer.data

                    url = json_data["url"]
                    marker_type = obj.type

                    if user.team is not None and distance_in_meters <= self.action_range:
                        logger.debug("[PLAY]: distance: '%f' <= action_range: '%f'", distance_in_meters, self.action_range)

                        if obj.type in [MARKER_TYPES.RED_FLAG, MARKER_TYPES.BLUE_FLAG]:
                            self.capture_the_flag(obj, user)

                    marker = Marker(
                        distance=distance_in_meters,
                        type=marker_type,
                        url=url,
                        location=obj.location)

                    logger.debug("marker was created: %s", marker)
                    markers.append(marker)
                elif isinstance(obj, PortalUser) and obj.id is not user.id:
                    serializer = PortalUserSerializer(obj, context=context)
                    json_data = serializer.data

                    url = json_data["url"]
                    marker_type = MARKER_TYPES.PLAYER

                    marker = Marker(
                        distance=distance_in_meters,
                        type=marker_type,
                        url=url,
                        location=obj.location)

                    logger.debug("marker was created: %s", marker)
                    markers.append(marker)
        return markers

    def capture_the_flag(self, flag, user):
        """ This method contains a logic for capture the flag from other user, enemy base or from another
        place on the map.
        """
        logger.debug("Trying to capture the flag...")

        if flag.last_captured_by == user:
            when_captured = flag.when_captured.replace(tzinfo=None)
            delta = (datetime.datetime.now() - when_captured).seconds
            logger.debug("delta: %d s", delta)
            if delta <= self.capture_time:
                # do nothing - user cannot captured the flag
                logger.debug("do nothing - user '%s' cannot captured the flag (delta: %s s", user.username, delta)
                return

        if user.team == PortalUser.TEAM_TYPES.RED_TEAM:
            if flag.type == MARKER_TYPES.RED_FLAG:
                flag.location = self.red_base.location
                logger.debug("[PLAY]: Red flag is going back to the red base (game: %d)", self.id)
            else:
                flag.location = user.location
                logger.debug("[PLAY]: Player: '%s' take a blue flag (game: %d)", user.username, self.id)

            flag.last_captured_by = user
            flag.when_captured = datetime.datetime.now()
            flag.save()
        else:
            if flag.type == MARKER_TYPES.BLUE_FLAG:
                flag.location = self.blue_base.location
                logger.debug("[PLAY]: Blue flag is going back to the blue base (game: %d)", self.id)
            else:
                flag.location = user.location
                logger.debug("[PLAY]: Player: '%s' take a red flag (game: %d)", user.username, self.id)
            flag.last_captured_by = user
            flag.when_captured = datetime.datetime.now()
            flag.save()

    def team_balancing(self):
        for idx, player in enumerate(self.players.all()):
            if idx % 2:
                player.team = PortalUser.TEAM_TYPES.RED_TEAM
            else:
                player.team = PortalUser.TEAM_TYPES.BLUE_TEAM
            player.save()

    def start(self):
        logger.info("Game: %s with %d players is starting...", self.id, self.name)

        if self.status == self.GAME_STATUSES.IN_PROGRESS:
            raise GameAlreadyStartedException()

        logger.info("Team balancing...")
        self.team_balancing()

        self.status = self.GAME_STATUSES.IN_PROGRESS
        self.save()

        # todo: send broadcast notification to all players

    def stop(self):
        logger.info("Game: %s with %d players is stopping...", self.id, self.name)

        if self.status == self.GAME_STATUSES.FINISHED:
            raise GameAlreadyFinishedException()

        self.status = self.GAME_STATUSES.FINISHED
        self.save()
        self.players.update(team=None)

        # todo: send broadcast notification to all players

    def _get_item(self, item_type):
        items = filter(lambda item: item.type == item_type, self.items.all())
        return items[0] if items else None

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"