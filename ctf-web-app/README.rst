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

How to run?
-----------
ToDo

System requirements
-------------------
sudo apt-get install libgeos++-dev libgeos-3.3.3 libgeos-c1 libgeos-dbg libgeos-dev libgeos-doc libgeos-ruby1.8


Curl example commands
---------------------

User registration:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "email": "mort@ctf.nete", "password": "mort"}' http://127.0.0.1:8000/api/registration/


User authentication:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "password": "mort", "device_type": "android", "device_id": "5432456-123456"}' http://127.0.0.1:8000/token/


Update user position:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 8da5bc37681ba38352cd7ea8bf88e4e762d72542" -XPATCH -d '{"lat": 53.429138, "lon": 14.556424}' http://127.0.0.1:8000/api/users/2/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 1b84fc8e06b7f759433889b087f594c7094ffa50" -XPATCH -d '{"lat": 53.322809, "lon": 14.538427}' http://127.0.0.1:8000/api/users/1/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 1b84fc8e06b7f759433889b087f594c7094ffa50" -XPATCH -d '{"lat": 53.322809, "lon": 14.538427}' http://127.0.0.1:8000/api/users/3/


Create a new map:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token b430e9a6f7495c26597fab4b1c3bb2af9a8e8ccc" -XPOST -d '{"name": "Jasne Blonia", "description": "", "radius": 2500, "location": {"lat": 53.440157, "lon": 14.540221}}' http://127.0.0.1:8000/api/maps/

Create a new game:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token b430e9a6f7495c26597fab4b1c3bb2af9a8e8ccc" -XPOST -d '{ "name": "CTF second test game", "description": "Test 2 game", "start_time": "2014-05-02T12:00:00", "max_players": 12, "status": 0, "type": 0, "map": "http://127.0.0.1:8000/api/maps/1/", "visibility_range": 1000.0, "action_range": 20.0, "players": [], "invited_users": ["http://127.0.0.1:8000/api/users/2/", "http://127.0.0.1:8000/api/users/1/"], "items": [] }' http://127.0.0.1:8000/api/games/


Get all games:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token b430e9a6f7495c26597fab4b1c3bb2af9a8e8ccc" -XGET http://127.0.0.1:8000/api/games/

