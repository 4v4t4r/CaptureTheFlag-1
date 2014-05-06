API: model *Map*
================

**Create** map
--------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Bearer {token}" -d '{ model } -X POST http://ctf.host/api/maps/

*response*:
::

    HTTP 201 Created
    {
        "url": "http://ctf.host/api/maps/1/",
        "author": "http://ctf.host/api/users/3/",
        "name": "Jasne Blonia",
        "description": "Super fajna gra",
        "radius": 2500.0,
        "lat": 53.440157,
        "lon": 14.540221,
        "games": [
            "http://ctf.host/api/games/1/"
        ]
    }

**Read** a single map
---------------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X GET http://ctf.host/api/maps/{map_id}/

*response*:
::

    HTTP 200 OK
    { model }


**Update** item
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model }' -X PUT http://ctf.host/api/maps/{map_id}/

*response*:
::

    HTTP 200 OK
    { model }

**Update** single map field
---------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model }' -X PATCH http://ctf.host/api/maps/{map_id}/

*response*:
::

    HTTP 200 OK

**Delete** map
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X DELETE http://ctf.host/api/maps/{map_id}/

*response*:
::

    HTTP 204 No Content


Example (response of selected map)
----------------------------------
::

    {
        "url": "http://localhost:8000/api/maps/1/",
        "author": "http://localhost:8000/api/users/3/",
        "name": "Jasne Blonia",
        "description": "",
        "radius": 2500.0,
        "lat": 53.440157,
        "lon": 14.540221,
        "games": [
            "http://localhost:8000/api/games/1/"
        ]
    }

