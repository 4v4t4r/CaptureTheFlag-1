from model_utils import Choices
from django.utils.translation import ugettext as _

__author__ = 'mkr'

GAME_STATUSES = Choices(
    (0, 'IN_PROGRESS', _('In progress')),
    (1, 'CREATED', _('Created')),
    (2, 'ON_HOLD', _('On hold')),
    (3, 'CANCELED', _('Canceled'))
)

GAME_TYPES = Choices(
    (0, 'FRAG_BASED', _('Frag based')),
    (1, 'TIME_BASED', _('Time based')),
)

ITEM_TYPES = Choices(
    (0, 'FLAG_RED', _('Red flag')),
    (1, 'FLAG_BLUE', _('Blue flag')),
    (2, 'BASE_RED', _('Red base')),
    (3, 'BASE_BLUE', _('Blue base')),
    (4, 'AID_KIT', _('First aid kit')),
    (5, 'PISTOL', _('Pistol')),
    (6, 'AMMO', _('Ammo')),
)