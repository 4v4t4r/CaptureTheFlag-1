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
        "url": "http://ctf.host/api/users/2/",
        "username": "frodo",
        "first_name": "",
        "last_name": "",
        "email": "frodo@ctf.net",
        "nick": "frodo",
        "active_character": "http://ctf.host/api/characters/5/",
        "characters": [
            "http://ctf.host/api/characters/5/",
            "http://ctf.host/api/characters/6/",
            "http://ctf.host/api/characters/7/",
            "http://ctf.host/api/characters/8/"
        ],
        "device_type": 0,
        "device_id": "5432456-123456",
        "location": {
            "lat": 55.12123,
            "lon": 14.1234
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
        "url": "http://localhost:8000/api/users/3/",
        "username": "mort",
        "first_name": "",
        "last_name": "",
        "email": "mort@ctf.nete",
        "nick": "mort",
        "active_character": "http://localhost:8000/api/characters/5/",
        "characters": [
            "http://localhost:8000/api/characters/12/",
            "http://localhost:8000/api/characters/9/",
            "http://localhost:8000/api/characters/10/",
            "http://localhost:8000/api/characters/11/"
        ],
        "device_type": 0,
        "device_id": "5432456-123456",
        "location": {
            "lat": 53.491824,
            "lon": 14.593852
        }
    }