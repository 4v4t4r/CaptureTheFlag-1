API: Token authentication
=========================
Token authentication process based on user name and password. During authentication user should pass an information
about device type and device id.

**Authentication**
------------------

*request*:
::

    -H "Accept: application/json" -H "Content-type: application/json" -XPOST -d '{"username": "{username}", "password": "{password}", "device_type": "android", "device_id": "14234-1234123-23423"}' http://ctf.host/token/

*response*:
::

    HTTP 200 OK
    {
      "token": {token},
      "user": {current_user_url}
    }

**Important!**
After correct authentication, you should always put into the request headers below header:
::

    -H "Authorization: Token {token}"

