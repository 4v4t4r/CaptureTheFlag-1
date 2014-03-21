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

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "frodo", "email": "frodo@ctf.nete", "password": "frodo"}' http://127.0.0.1:8000/api/registration/


User authentication:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "mort", "password": "mort"}' http://127.0.0.1:8000/token/


Update user position:

::

    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 1b84fc8e06b7f759433889b087f594c7094ffa50" -XPATCH -d '{"lat": 53.429138, "lon": 14.556424}' http://127.0.0.1:8000/api/users/2/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 1b84fc8e06b7f759433889b087f594c7094ffa50" -XPATCH -d '{"lat": 53.322809, "lon": 14.538427}' http://127.0.0.1:8000/api/users/1/
    curl -H "Accept: application/json" -H "Content-type: application/json" -H "Authorization: Token 1b84fc8e06b7f759433889b087f594c7094ffa50" -XPATCH -d '{"lat": 53.322809, "lon": 14.538427}' http://127.0.0.1:8000/api/users/3/
