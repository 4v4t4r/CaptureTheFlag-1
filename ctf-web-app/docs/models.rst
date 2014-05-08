Models definitions
==================

Model: Marker
-------------

Types mapping:
::

    MARKER_TYPES = [
        0 - 'PLAYER',
        1 - 'PLAYER_WITH_RED_FLAG',
        2 - 'PLAYER_WITH_BLUE_FLAG',

        3 - 'RED_FLAG',
        4 - 'BLUE_FLAG',

        5 - 'RED_BASE',
        6 - 'BLUE_BASE',

        7 - 'RED_BASE_WITH_FLAG',
        8 - 'BLUE_BASE_WITH_FLAG',

        9 - 'FIRST_AID_KIT',
        10 - 'PISTOL',
        11 - 'AMMO',
    ]

Model:
::

    {
        marker_type: int,  # choices=MARKER_TYPES
        distance: float,
        location: {
            lat: float,
            lon: float
        }
        url: string
    }

Model: Item
-----------

Types mapping:
::

    MARKER_TYPES = [
        3 - 'RED_FLAG',
        4 - 'BLUE_FLAG',

        5 - 'RED_BASE',
        6 - 'BLUE_BASE',

        7 - 'RED_BASE_WITH_FLAG',
        8 - 'BLUE_BASE_WITH_FLAG',

        9 - 'FIRST_AID_KIT',
        10 - 'PISTOL',
        11 - 'AMMO',
    ]

Model:
::

    {
        name: string # (required=True, max_length=50)
        description: string # (max_length=200)
        type: int # (choices=ITEM_TYPES)
        location: { # (required=False)
            lat: float
            lon: float
        }
        value: float
    }

API `definition <./api/item.rst>`_.

Model: User
-----------

Device types mapping:
::

    DEVICE_TYPES = {
        0 - 'ANDROID',
        1 - 'WP',
        2 - 'IOS'
    }

Team types mapping:
::

    TEAM_TYPES = {
        0 - 'READ TEAM',
        1 - 'BLUE TEAM'
    }

Model:
::

    {
        url: string # url to current resource
        username: string # (required=True, max_length=50, unique=True)
        email: string # (required=True, max_length=50)
        password: string # (required=True, min_length=6, max_length=50)
        nick: string # (required=False, max_length=100)
        device_type: int (required=False)
        device_id: string (required=False, max_length=255)
        location: { # (required=False)
            lat: float
            lon: float
        }
        team: int (required=False)
        characters = [ ] # list of url for characters objects
    }

API `definition <./api/user.rst>`_.

Model: Game
-----------

Statuses mapping:
::

    GAME_STATUSES = [
        0 - 'In progress',
        1 - 'Created',
        2 - 'On hold',
        3 - 'Canceled',
        4 - 'Finished',
    ]

Types mapping:
::

    GAME_TYPE = [
        0 - 'Frags',
        1 - 'Time',
    ]

Model:
::

    {
        url: string # url for current resource
        name: string # (required=True, max_length=100)
        description: string # (null=True, blank=True, max_length=255)
        start_time: date_time
        max_players: int
        status: int # (choices=GAME_STATUSES)
        type: int # (choices=GAME_TYPE)
        radius: float # in meters
        location: {
            lat: float,
            lon: float
        }
        visibility_range: float
        action_range: float
        players: [] # urls for players objects (object: User)
        invited_users: [] # urls for invited users objects (object PortalUser)
        items: [] # urls for items objects (object: Item)
        owner: string # read_only=True, url for user
        last_modified: date_time # read_only=True, format:"YYYY-MM-DDTHH:MM:SS"
        created: date_time # read_only=True, format:"YYYY-MM-DDTHH:MM:SS"
    }

API `definition <./api/game.rst>`_.