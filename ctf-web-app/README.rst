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

User registration:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "email": "mort@ctf.host", "password": "mort"}' http://127.0.0.1:8000/api/registration/


User authentication:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "password": "mort", "device_type": 0, "device_id": "000000-00000-1"}' http://127.0.0.1:8000/token/


Update user properties:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -XPATCH -d '{"location": {"lat": 53.440396, "lon": 14.539494}}' http://127.0.0.1:8000/api/users/2/



Create a new game:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -XPOST -d '{ "name": "CTF second test game", "description": "Test game 1", "radius": 2500, "location": {"lat": 53.440157, "lon": 14.540221}, "start_time": "2014-05-02T12:00:00", "max_players": 12, "status": 0, "type": 0, "map": "http://127.0.0.1:8000/api/maps/1/", "visibility_range": 1000.0, "action_range": 20.0, "players": [], "invited_users": ["http://127.0.0.1:8000/api/users/2/"], "items": [] }' http://127.0.0.1:8000/api/games/


Get all games:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -XGET http://127.0.0.1:8000/api/games/

Get selected game:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -XGET http://127.0.0.1:8000/api/games/1/

Add player to the selected game:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -XPOST http://127.0.0.1:8000/api/games/1/player/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 07ef9cc82691da43233cb24809177339cde726dc" -XPOST http://127.0.0.1:8000/api/games/1/player/

Remove player from the selected game:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -XDELETE http://127.0.0.1:8000/api/games/1/player/

Player's position registration:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token c1ab13720545f202466104710fa61a5f1de41c11" -d '{"lat": 53.440460, "lon": 14.540911}' -XPUT http://127.0.0.1:8000/api/games/1/location/
