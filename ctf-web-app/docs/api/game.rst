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
        "url": "http://127.0.0.1:8000/api/games/1/",
        "name": "CTF second test game",
        "description": "Test game 1",
        "start_time": "2014-05-02T12:00:00",
        "max_players": 12,
        "status": 0,
        "type": 0,
        "radius": 2500.0,
        "location": {
            "lat": 53.440157,
            "lon": 14.540221
        },
        "visibility_range": 1000.0,
        "action_range": 20.0,
        "players": [],
        "invited_users": [
            "http://127.0.0.1:8000/api/users/2/"
        ],
        "items": [],
        "owner": "http://127.0.0.1:8000/api/users/2/",
        "last_modified": "2014-05-06T12:18:58.216Z",
        "created": "2014-05-06T12:18:58.216Z"
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
        "url": "http://127.0.0.1:8000/api/games/1/",
        "name": "CTF second test game",
        "description": "Test game 1",
        "start_time": "2014-05-02T12:00:00",
        "max_players": 12,
        "status": 0,
        "type": 0,
        "radius": 2500.0,
        "location": {
            "lat": 53.440157,
            "lon": 14.540221
        },
        "visibility_range": 1000.0,
        "action_range": 20.0,
        "players": [],
        "invited_users": [
            "http://127.0.0.1:8000/api/users/2/"
        ],
        "items": [],
        "owner": "http://127.0.0.1:8000/api/users/2/",
        "last_modified": "2014-05-06T12:18:58.216Z",
        "created": "2014-05-06T12:18:58.216Z"
    }

