from django.utils.translation import ugettext_lazy as _
from model_utils import Choices

__author__ = 'mkr'

DEVICE_TYPES = Choices(
    (0, 'ANDROID', _("Android")),
    (1, 'WP', _("Windows Phone")),
    (2, 'IOS', _("iOS")),
)

TEAM_TYPES = Choices(
    (0, 'RED_TEAM', _("Red team")),
    (1, 'BLUE_TEAM', _("Blue team")),
)

CHARACTER_TYPES = (
    (0, _('Private')),
    (1, _('Medic')),
    (2, _('Commandos')),
    (3, _('Spy')),
)

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

ITEM_TYPES = MARKER_TYPES[3:]

GAME_STATUSES = Choices(
    (0, 'CREATED', _('Created')),
    (1, 'IN_PROGRESS', _('In progress')),
    (2, 'ON_HOLD', _('On hold')),
    (3, 'CANCELED', _('Canceled')),
    (4, 'FINISHED', _('Finished')),
)

GAME_TYPES = Choices(
    (0, 'BASED_ON_POINTS', _('Based on points')),
    (1, 'BASED_ON_TIME', _('Based on time')),
)

NOTIFICATION_TYPE = Choices(
    (0, 'START_GAME', _("Start game")),
    (1, 'STOP_GAME', _("Stop game")),
    (2, 'CAPTURED_RED_FLAG', _("Captured the red flag")),
    (3, 'CAPTURED_BLUE_FLAG', _("Captured the red flag")),
)
