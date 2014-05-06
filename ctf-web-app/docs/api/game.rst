API: model *Game*
=================

**Create** game
---------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model } -X POST http://ctf.host/api/games/

*response*:
::

    HTTP 201 Created
    {
        "url": "http://ctf.host/api/games/1/",
        "name": "Capture the Flag",
        "description": "",
        "start_time": "2014-02-28T16:00:00Z",
        "max_players": 12,
        "status": 0,
        "type": 1,
        "map": "http://ctf.host/api/maps/1/",
        "visibility_range": 200.0,
        "action_range": 5.0,
        "players": [],
        "invited_users": []
    }

**Read** game
-------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X GET http://ctf.host/api/games/{game_id}/

*response*:
::

    HTTP 200 OK
    { model }


**Update** game
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model }' -X PUT http://ctf.host/api/games/{game_id}/

*response*:
::

    HTTP 200 OK
    { model }

**Update** single game field
----------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model.fields }' -X PATCH http://ctf.host/api/games/{game_id}/

*response*:
::

    HTTP 200 OK

**Delete** game
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X DELETE http://ctf.host/api/games/{game_id}/

*response*:
::

    HTTP 204 No Content


Example (response of selected game)
-----------------------------------
::

    {
        "url": "http://localhost:8000/api/games/1/",
        "name": "Capture the Flag",
        "description": "",
        "start_time": "2014-02-28T16:00:00Z",
        "max_players": 12,
        "status": 0,
        "type": 1,
        "map": "http://localhost:8000/api/maps/1/",
        "visibility_range": 200.0,
        "action_range": 5.0,
        "players": [
            "http://localhost:8000/api/characters/11/",
            "http://localhost:8000/api/characters/8/",
            "http://localhost:8000/api/characters/1/"
        ],
        "invited_users": [],
        "items": [
            "http://localhost:8000/api/items/2/",
            "http://localhost:8000/api/items/1/"
        ]
    }

