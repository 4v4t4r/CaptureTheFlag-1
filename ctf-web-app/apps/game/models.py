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

    def __init__(self, name=None, description=None, status=STATUS.CREATED, start_time=None, max_players=0,
                 type=TYPE.FRAG_BASED, players=None, items=None, map=None):
        self.name = name
        self.description = description
        self.status = status
        self.start_time = start_time
        self.max_players = max_players
        self.type = type
        self.players = players
        self.items = items
        self.map = map


class Map(object):
    def __init__(self, name=None, description=None, location=None, radius=None, author=None):
        self.name = name
        self.description = description
        self.location = location
        self.radius = radius
        self.author = author


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

    def __init__(self, name=None, description=None, type=None, location=None, value=None):
        self.name = name
        self.description = description
        self.type = type
        self.location = location
        self.value = value
