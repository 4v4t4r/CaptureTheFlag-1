API: model *User*
=================

**Read** a single user
----------------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X GET http://ctf.host/api/users/{id}/

*response*:
::

    HTTP 200 OK
    {
        "url": "http://localhost:8000/api/users/2/",
        "username": "mort",
        "email": "mort@ctf.host",
        "nick": "mort",
        "team": 0,
        "current_game_id": 1,
        "device_type": 0,
        "device_id": "000000-00000-1",
        "location": {
            "lat": 53.438758,
            "lon": 14.541617
        }
    }

**Update** user
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model }' -X PUT http://ctf.host/api/users/{id}/

*response*:
::

    HTTP 200 OK

**Update** single user's field
------------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model.fields }' -X PATCH http://ctf.host/api/users/{id}/

*response*:
::

    HTTP 200 OK

**Delete** user
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X DELETE http://ctf.host/api/users/{id}/

*response*:
::

    HTTP 204 No Content


Example (response of selected user)
-----------------------------------
::

    {
        "url": "http://localhost:8000/api/users/2/",
        "username": "mort",
        "email": "mort@ctf.host",
        "nick": "mort",
        "team": 0,
        "current_game_id": 1,
        "device_type": 0,
        "device_id": "000000-00000-1",
        "location": {
            "lat": 53.438758,
            "lon": 14.541617
        }
    }
