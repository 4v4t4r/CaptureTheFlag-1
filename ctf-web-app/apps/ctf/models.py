from model_utils import Choices
from django.utils.translation import ugettext_lazy as _
from django.db import models
from apps.core.models import PortalUser, Character


class Item(models.Model):
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

    # location
    lat = models.FloatField(verbose_name=_("Latitude"))
    lon = models.FloatField(verbose_name=_("Longitude"))

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"


class Map(models.Model):
    name = models.CharField(max_length=100, verbose_name=_("Name"))
    description = models.TextField(null=True, max_length=255, verbose_name=_("Description"))
    radius = models.FloatField(verbose_name=_("Radius"))

    author = models.ForeignKey(PortalUser, null=True, verbose_name=_("Author"))

    # location
    lat = models.FloatField(verbose_name=_("Latitude"))
    lon = models.FloatField(verbose_name=_("Longitude"))

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
    players = models.ManyToManyField(Character, verbose_name=_("Players"), related_name='games')
    items = models.ManyToManyField(Item, verbose_name=_("Items"), related_name='games')

    def __unicode__(self):
        return "%s" % self.name

    class Meta:
        app_label = "ctf"