from django.db import models
from model_utils import Choices
from django.utils.translation import ugettext as _


class Game(object):
    STATUS = Choices(
        (0, 'IN_PROGRESS', _('In progress')),
        (1, 'CREATED', _('Created')),
        (2, 'ON_HOLD', _('On hold')),
        (3, 'CANCELED', _('Canceled'))
    )

    TYPE = Choices(
        (0, 'FRAG_BASED', _('Frag based')),
        (1, 'TIME_BASED', _('Time based')),
    )

    name = models.CharField(max_length=50)
    description = models.CharField(max_length=200)
    status = models.IntegerField(choices=STATUS, default=STATUS.CREATED)
    start_time = models.DateField()
    max_players = models.IntegerField()
    type = models.IntegerField(choices=TYPE, default=TYPE.FRAG_BASED)
    players = None
    items = None
    map = None


class Map(object):
    name = models.CharField(max_length=50)
    description = models.CharField(max_length=200)
    location = None
    radius = models.IntegerField()
    author = None


class Item(object):
    TYPE = Choices(
        (0, 'FLAG_RED', _('Red flag')),
        (1, 'FLAG_BLUE', _('Blue flag')),
        (2, 'BASE_RED', _('Red base')),
        (3, 'BASE_BLUE', _('Blue base')),
        (4, 'AID_KIT', _('First aid kit')),
        (5, 'PISTOL', _('Pistol')),
        (6, 'AMMO', _('Ammo')),
    )

    name = models.CharField(max_length=50)
    description = models.CharField(max_length=200)
    type = models.IntegerField(choices=TYPE)
    location = None
    value = None
