import logging
from model_utils import Choices
from haystack.query import SearchQuerySet
from haystack.utils.geo import Point, D
from django.utils.translation import ugettext_lazy as _
from django.db import models
from apps.core.models import PortalUser, Character, GeoModel

logger = logging.getLogger("root")


class Item(GeoModel):
    ITEM_TYPES = Choices(
        (0, 'FLAG_RED', _('Red flag')),
        (1, 'FLAG_BLUE', _('Blue flag')),
        (2, 'BASE_RED', _('Red base')),
        (3, 'BASE_BLUE', _('Blue base')),
        (4, 'AID_KIT', _('First aid kit')),
        (5, 'PISTOL', _('Pistol')),
        (6, 'AMMO', _('Ammo')),
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


class Map(GeoModel):
    name = models.CharField(max_length=100, verbose_name=_("Name"))
    description = models.TextField(null=True, blank=True, max_length=255, verbose_name=_("Description"))
    radius = models.FloatField(verbose_name=_("Radius"))

    author = models.ForeignKey(PortalUser, null=True, blank=True, verbose_name=_("Author"))

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"


class Game(models.Model):
    GAME_STATUSES = Choices(
        (0, 'CREATED', _('Created')),
        (1, 'IN_PROGRESS', _('In progress')),
        (2, 'ON_HOLD', _('On hold')),
        (3, 'CANCELED', _('Canceled'))
    )

    GAME_TYPES = Choices(
        (0, 'FRAG_BASED', _('Frag based')),
        (1, 'TIME_BASED', _('Time based')),
    )

    name = models.CharField(max_length=100, verbose_name=_("Name"))
    description = models.TextField(null=True, blank=True, max_length=255, verbose_name=_("Description"))
    start_time = models.DateTimeField(verbose_name=_("Start time"))
    max_players = models.IntegerField(null=True, blank=True, verbose_name=_("Max players"))
    status = models.IntegerField(choices=GAME_STATUSES, default=GAME_STATUSES.CREATED, verbose_name=_("Status"))
    type = models.IntegerField(choices=GAME_TYPES, default=GAME_TYPES.FRAG_BASED, verbose_name=_("Type"))
    map = models.ForeignKey(Map, verbose_name=_("Map"), related_name='games')

    visibility_range = models.FloatField(default=200.00, verbose_name=_("Visibility range"))  # in meters
    action_range = models.FloatField(default=5.00, verbose_name=_("Action range"))  # in meters

    players = models.ManyToManyField(Character, verbose_name=_("Players"), related_name="joined_games")
    invited_users = models.ManyToManyField(PortalUser, verbose_name=_("Invited users"), related_name="pending_games")

    def add_player(self, user):
        character = user.get_active_character()

        if character is None:
            raise AssertionError("An active character is not defined")

        if self.max_players and self.max_players == self.players.count():
            raise Exception("Max value of players exceeded")

        logger.debug("character.user: %s", character.user)

        is_user_already_exist = self.players.filter(user=user).exists()
        logger.debug("is_user_already_exist: %s", is_user_already_exist)

        if is_user_already_exist:
            logger.info("User '%s' already joined into the game '%s'...", user.username, self.name)
        else:
            logger.info("User '%s' in character '%s' is joining into the game '%s'...", user.username, character.type,
                        self.name)
            self.players.add(character)
            self.save()
        return character

    def remove_player(self, user):
        character = user.get_active_character()

        if character is None:
            raise AssertionError("An active character is not defined")

        logger.info("User '%s' in character '%s' is removing from game '%s'...", user.username, character.type,
                    self.name)
        self.players.remove(character)
        self.save()

        return character

    def get_neighbours(self, user):
        location = (user.lat, user.lon)
        point = Point(location[1], location[0])
        max_dist = D(m=self.visibility_range)

        logger.debug("user: %s", user)
        logger.debug("location: %s", location)
        logger.debug("max_dist: %s", max_dist)

        sqs = SearchQuerySet().dwithin('location', point, max_dist)

        logger.debug("Query: %s", sqs.query)

        return sqs

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"