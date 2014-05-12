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
        "url": "http://localhost:8000/api/games/1/",
        "name": "CTF first test game",
        "description": "Test game 1",
        "start_time": "2014-05-02T10:00:00Z",
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
        "players": [
            "http://localhost:8000/api/users/2/",
            "http://localhost:8000/api/users/3/"
        ],
        "invited_users": [
            "http://localhost:8000/api/users/2/",
            "http://localhost:8000/api/users/3/"
        ],
        "items": [
            "http://localhost:8000/api/items/6/",
            "http://localhost:8000/api/items/5/",
            "http://localhost:8000/api/items/2/",
            "http://localhost:8000/api/items/1/"
        ],
        "owner": "http://localhost:8000/api/users/2/",
        "last_modified": "2014-05-08T10:15:35.303Z",
        "created": "2014-05-08T10:06:32.210Z"
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
        "name": "CTF first test game",
        "description": "Test game 1",
        "start_time": "2014-05-02T10:00:00Z",
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
        "players": [
            "http://localhost:8000/api/users/2/",
            "http://localhost:8000/api/users/3/"
        ],
        "invited_users": [
            "http://localhost:8000/api/users/2/",
            "http://localhost:8000/api/users/3/"
        ],
        "items": [
            "http://localhost:8000/api/items/6/",
            "http://localhost:8000/api/items/5/",
            "http://localhost:8000/api/items/2/",
            "http://localhost:8000/api/items/1/"
        ],
        "owner": "http://localhost:8000/api/users/2/",
        "last_modified": "2014-05-08T10:15:35.303Z",
        "created": "2014-05-08T10:06:32.210Z"
    }

**Add player to the selected game**
-----------------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -XPOST http://ctf.host/api/games/{game_id}/player/

*response*:
::

    HTTP 200 OK

*response (if user already joined)*:
::

    HTTP 400 BAD REQUEST

    {
        "error": "User 'frodo' already joined into the game '1: CTF test game 1'"
    }

**Remove player from the selected game**
----------------------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -XDELETE http://ctf.host/api/games/{game_id}/player/

*response*:
::

    HTTP 200 OK

*response (if user doesn't exist in the selected game)*:
::

    HTTP 404 NOT FOUND

    {
        "error": "User 'mort' doesn't exist in selected game '1: CTF test game 1'"
    }

**Player's position registration**
----------------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{"lat": <lat>, "lon": <lon>}' -XPOST http://ctf.host/api/games/{game_id}/location/

*response*:
::


    HTTP 200 OK

    {
        "game": {
            "red_team_points": 0,
            "blue_team_points": 1,
            "time_to_end": 7397,
            "status": 1
        },
        "markers": [
            {
                "type": 6,
                "distance": 317.34338081067676,
                "url": "http://127.0.0.1:8000/api/items/4/",
                "location": {
                    "lat": 53.438732,
                    "lon": 14.541759
                }
            },
            {
                "type": 5,
                "distance": 0.8558529589657696,
                "url": "http://127.0.0.1:8000/api/items/3/",
                "location": {
                    "lat": 53.441168,
                    "lon": 14.539277
                }
            }
        ]
    }

