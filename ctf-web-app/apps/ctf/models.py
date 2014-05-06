import logging
from haystack.query import SearchQuerySet
from haystack.utils.geo import Point, D
from django.utils.translation import ugettext_lazy as _
from django.db import models
from apps.core.exceptions import AlreadyExistException, GameAlreadyStartedException
from apps.core.models import PortalUser, GeoModel

logger = logging.getLogger("root")


class Item(GeoModel):
    ITEM_TYPES = (
        (0, _('Red flag')),
        (1, _('Blue flag')),
        (2, _('Red base')),
        (3, _('Blue base')),
        (4, _('First aid kit')),
        (5, _('Pistol')),
        (6, _('Ammo')),
    )

    name = models.CharField(max_length=100, verbose_name=_("Name"))
    type = models.IntegerField(choices=ITEM_TYPES, verbose_name=_("Type"))
    value = models.FloatField(verbose_name=_("Value"))
    description = models.TextField(null=True, max_length=255, verbose_name=_("Description"))
    game = models.ForeignKey("Game", related_name="items", verbose_name=_("Game"))

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"


class Game(GeoModel):
    GAME_STATUSES = (
        (0, _('Created')),
        (1, _('In progress')),
        (2, _('On hold')),
        (3, _('Canceled'))
    )

    GAME_TYPES = (
        (0, _('Frag based')),
        (1, _('Time based')),
    )

    name = models.CharField(max_length=100, verbose_name=_("Name"))
    description = models.TextField(null=True, blank=True, max_length=255, verbose_name=_("Description"))

    radius = models.FloatField(verbose_name=_("Radius"))
    start_time = models.DateTimeField(verbose_name=_("Start time"))
    max_players = models.IntegerField(null=True, blank=True, verbose_name=_("Max players"))
    status = models.IntegerField(choices=GAME_STATUSES, default=GAME_STATUSES[0][0], verbose_name=_("Status"))
    type = models.IntegerField(choices=GAME_TYPES, default=GAME_TYPES[0][0], verbose_name=_("Type"))

    visibility_range = models.FloatField(default=200.00, verbose_name=_("Visibility range"))  # in meters
    action_range = models.FloatField(default=5.00, verbose_name=_("Action range"))  # in meters

    players = models.ManyToManyField(PortalUser, verbose_name=_("Players"), related_name="joined_games")
    invited_users = models.ManyToManyField(PortalUser, verbose_name=_("Invited users"), related_name="pending_games")

    owner = models.ForeignKey(PortalUser, null=True, blank=True, verbose_name=_("Owner"))
    last_modified = models.DateTimeField(auto_now=True, verbose_name=_("Last modified"))
    created = models.DateTimeField(auto_now_add=True, verbose_name=_("Created"))

    def add_player(self, user):
        active_character = user.active_character

        if active_character is None:
            raise AssertionError("An active character is not defined")

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
            logger.info("User '%s' in character '%s' is joining into the game '%s'...", user.username,
                        active_character.type, self.name)
            self.players.add(user)
            self.save()

    def remove_player(self, user):
        active_character = user.active_character

        if not self.players.filter(pk=user.id).exists():
            raise PortalUser.DoesNotExist("User '%s' doesn't exist in selected game '%s: %s'" % (
                user.username, self.id, self.name))

        self.players.remove(user)
        logger.info("User '%s' in character '%s' is removing from game '%s'...", user.username, active_character.type,
                    self.name)
        self.save()

    def get_neighbours(self, user):
        location = user.location
        point = Point(location.lon, location.lat)
        max_dist = D(m=self.visibility_range)

        logger.debug("user: %s", user)
        logger.debug("location: %s", location)
        logger.debug("max_dist: %s", max_dist)

        sqs = SearchQuerySet().dwithin('location', point, max_dist).distance('location', point)

        logger.debug("Query: %s", sqs.query)
        logger.debug("Query distance: %s", sqs.distance)

        return sqs

    def team_balancing(self):
        for idx, player in iter(self.players):
            if idx % 0:
                player.team = PortalUser.TEAM_TYPES[0][0]
            else:
                player.team = PortalUser.TEAM_TYPES[1][0]
            player.save()

    def start(self):
        logger.info("Game: %d: %s with %d players is starting...", self.id, self.name)

        if self.status == self.GAME_STATUSES[1][0]:  # in progress
            raise GameAlreadyStartedException()

        logger.info("Team balancing...")
        self.team_balancing()

    def stop(self):
        pass

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"