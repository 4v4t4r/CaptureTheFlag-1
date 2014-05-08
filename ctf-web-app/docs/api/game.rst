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
        "players": [],
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

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{"lat": <lat>, "lon": <lon>}' -XPUT http://ctf.host/api/games/{game_id}/location/

*response*:
::

    HTTP 200 OK

    [
        {
            "marker_type": 4,
            "distance": 306.4619143264994,
            "url": "http://127.0.0.1:8000/api/items/2/",
            "location": {
                "lat": 53.438732,
                "lon": 14.541759
            }
        },
        {
            "marker_type": 0,
            "distance": 219.07368062567056,
            "url": "http://127.0.0.1:8000/api/users/3/",
            "location": {
                "lat": 53.43943,
                "lon": 14.541156
            }
        },
        {
            "marker_type": 3,
            "distance": 19.391233474360988,
            "url": "http://127.0.0.1:8000/api/items/1/",
            "location": {
                "lat": 53.441168,
                "lon": 14.539277
            }
        }
    ]




