ctf-web-app
===========

.. contents::

Description
-----------
Server side application with web interface and API.


Installation
------------
ToDo

Bootstrap
---------
Run below commands:

::

    ./manage.py syncdb
    ./manage.py migrate apps.core
    ./manage.py migrate apps.ctf
    ./manage.py migrate rest_framework.authtoken
    ./manage.py createsuperuser

How to run?
-----------
ToDo

System requirements
-------------------
sudo apt-get install libgeos++-dev libgeos-3.3.3 libgeos-c1 libgeos-dbg libgeos-dev libgeos-doc libgeos-ruby1.8


Documentation
-------------

See models `documentation <./docs/models.rst>`_.

See token authentication `documentation <./docs/auth.rst>`_.


Curl example commands
---------------------

mort:  58c4c9a63946f2519918bca1986d894f84cc382f
frodo: 051dd761944dc00c31fc9aa4dffa56b215fb2941

User registration:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "email": "mort@ctf.host", "password": "mort"}' http://127.0.0.1:8000/api/registration/
    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "frodo", "email": "frodo@ctf.host", "password": "frodo"}' http://127.0.0.1:8000/api/registration/


User authentication:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "password": "mort", "device_type": 0, "device_id": "000000-00000-1"}' http://127.0.0.1:8000/token/
    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "frodo", "password": "frodo", "device_type": 0, "device_id": "000000-00000-2"}' http://127.0.0.1:8000/token/


Update user properties:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPATCH -d '{"location": {"lat": 53.440396, "lon": 14.539494}}' http://127.0.0.1:8000/api/users/2/


Create a new game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "CTF first test game", "description": "Test game 1", "radius": 2500, "location": {"lat": 53.440157, "lon": 14.540221}, "start_time": "2014-05-02T12:00:00", "max_players": 12, "status": 0, "type": 0, "map": "http://127.0.0.1:8000/api/maps/1/", "visibility_range": 1000.0, "action_range": 20.0, "players": [], "invited_users": ["http://127.0.0.1:8000/api/users/2/", "http://127.0.0.1:8000/api/users/3/"], "items": [] }' http://127.0.0.1:8000/api/games/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "CTF second test game", "description": "Test game 2", "radius": 2500, "location": {"lat": 53.447545, "lon": 14.535383}, "start_time": "2014-05-02T12:00:00", "max_players": 12, "status": 0, "type": 0, "map": "http://127.0.0.1:8000/api/maps/1/", "visibility_range": 1000.0, "action_range": 20.0, "players": [], "invited_users": ["http://127.0.0.1:8000/api/users/2/", "http://127.0.0.1:8000/api/users/3/"], "items": [] }' http://127.0.0.1:8000/api/games/


Add new item into the selected game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Red flag", "type": "3",  "location": {"lat": 53.441168, "lon": 14.539277}, "game": "http://127.0.0.1:8000/api/games/1/" }' http://127.0.0.1:8000/api/items/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Blue flag", "type": "4",  "location": {"lat": 53.438732, "lon": 14.541759}, "game": "http://127.0.0.1:8000/api/games/1/" }' http://127.0.0.1:8000/api/items/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Red base", "type": "5",  "location": {"lat": 53.441168, "lon": 14.539277}, "game": "http://127.0.0.1:8000/api/games/1/" }' http://127.0.0.1:8000/api/items/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Blue base", "type": "6",  "location": {"lat": 53.438732, "lon": 14.541759}, "game": "http://127.0.0.1:8000/api/games/1/" }' http://127.0.0.1:8000/api/items/


    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Red flag", "type": "3",  "location": {"lat": 53.446751, "lon": 14.530256}, "game": "http://127.0.0.1:8000/api/games/2/" }' http://127.0.0.1:8000/api/items/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Blue flag", "type": "4",  "location": {"lat": 53.447364, "lon": 14.539708}, "game": "http://127.0.0.1:8000/api/games/2/" }' http://127.0.0.1:8000/api/items/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Red base", "type": "5",  "location": {"lat": 53.446751, "lon": 14.530256}, "game": "http://127.0.0.1:8000/api/games/2/" }' http://127.0.0.1:8000/api/items/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST -d '{ "name": "Blue base", "type": "6",  "location": {"lat": 53.447364, "lon": 14.539708}, "game": "http://127.0.0.1:8000/api/games/2/" }' http://127.0.0.1:8000/api/items/

Update an existing game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPATCH -d '{ "name": "CTF first test game"}' http://127.0.0.1:8000/api/games/1/

Get all games:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XGET http://127.0.0.1:8000/api/games/

Get selected game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XGET http://127.0.0.1:8000/api/games/1/

Add player to the selected game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST http://127.0.0.1:8000/api/games/1/player/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 051dd761944dc00c31fc9aa4dffa56b215fb2941" -XPOST http://127.0.0.1:8000/api/games/1/player/

Remove player from the selected game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XDELETE http://127.0.0.1:8000/api/games/1/player/

Player's position registration:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -d '{"lat": 53.441155, "lon": 14.539568}' -XPUT http://127.0.0.1:8000/api/games/1/location/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 051dd761944dc00c31fc9aa4dffa56b215fb2941" -d '{"lat": 53.439430, "lon": 14.541156}' -XPUT http://127.0.0.1:8000/api/games/1/location/

Start selected game:
::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 58c4c9a63946f2519918bca1986d894f84cc382f" -XPOST http://127.0.0.1:8000/api/games/1/start/

