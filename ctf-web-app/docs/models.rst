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
        active_character: string # url to active character
        characters = [ ] # list of url for characters objects
    }

API `definition <./api/user.rst>`_.

Model: Character (from version: 2.0)
------------------------------------

Types mapping:
::

    CHARACTER_TYPES = [
        0 - 'Private',
        1 - 'Medic',
        2 - 'Commandos',
        3 - 'Spy'
]

Model:
::

    {
        url: string # url to current resource
        user: string # url for user object
        type: int # (choices=CHARACTER_TYPES)
        total_time: int
        total_score: int
        health: float
        level: int
        is_active: boolean
    }

API `definition <./api/character.rst>`_.

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
        map: string # url for map object
        visibility_range: float
        action_range: float
        players: [] # urls for players objects (object Character)
        invited_users: [] # urls for invited users objects (object PortalUser)
    }

API `definition <./api/game.rst>`_.

Model: Map
----------

Model:
::

    {
        url: string # url for current resource
        name: string # (required=True, max_length=100)
        description: string # (null=True, max_length=255)
        radius: float
        author: string # url for user object
        lat: float
        lon: float
        games: [] # list of urls to games objects
    }

API `definition <./api/map.rst>`_.