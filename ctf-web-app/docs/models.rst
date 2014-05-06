Models definitions
==================

Model: Item
-----------

Types mapping:
::

    ITEM_TYPES = [
        0 - 'Red flag',
        1 - 'Blue flag',
        2 - 'Red base',
        3 - 'Blue base',
        4 - 'Medic box',
        5 - 'Pistol',
        6 - 'Ammo',
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
        players: [] # urls for players objects (object Character)
        invited_users: [] # urls for invited users objects (object PortalUser)
        owner: string # read_only=True, url for user
        last_modified: date_time # read_only=True, format:"YYYY-MM-DDTHH:MM:SS"
        created: date_time # read_only=True, format:"YYYY-MM-DDTHH:MM:SS"
    }

API `definition <./api/game.rst>`_.