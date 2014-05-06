API: model 'Item'
=================

**Create** item
---------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{model}' -X POST http://ctf.host/api/items/

*response*:
::

    HTTP 201 Created
    {
        "url": "http://ctf.host/api/items/1/",
        "name": "Red team flag",
        "type": 0,
        "value": 0.0,
        "description": "Red team flag",
        "lat": 53.442764,
        "lon": 14.537692,
        "game": "http://ctf.host/api/games/1/"
    }

**Read** a single item
----------------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X GET http://ctf.host/api/items/{id}/

*response*:
::

    HTTP 200 OK
    { model }


**Update** item
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model }' -X PUT http://ctf.host/api/items/{id}/

*response*:
::

    HTTP 200 OK
    { model }

**Update** single item field
----------------------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -d '{ model.fields }' -X PATCH http://ctf.host/api/items/{id}/

*response*:
::

    HTTP 200 OK

**Delete** item
---------------
*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token {token}" -X DELETE http://ctf.host/api/items/{id}/

*response*:
::

    HTTP 204 No Content


Example (response of selected item)
-----------------------------------
::

    {
        "url": "http://localhost:8000/api/items/2/",
        "name": "Blue team flag",
        "type": 1,
        "value": 0.0,
        "description": "Blue team flag",
        "lat": 53.442759,
        "lon": 14.537699,
        "game": "http://localhost:8000/api/games/1/"
    }

