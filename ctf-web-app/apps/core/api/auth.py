import logging
from django.contrib.auth import authenticate
from rest_framework import serializers
from rest_framework.views import APIView
from rest_framework import status
from rest_framework import parsers
from rest_framework import renderers
from rest_framework.response import Response
from rest_framework.authtoken.models import Token
from apps.core.api.serializers import PortalUserSerializer
from apps.core.models import PortalUser

__author__ = 'mkr'

logger = logging.getLogger('root')


class AuthTokenSerializer(serializers.Serializer):
    username = serializers.CharField()
    password = serializers.CharField()
    device_type = serializers.CharField()
    device_id = serializers.CharField()

    def validate(self, attrs):
        username = attrs.get('username')
        password = attrs.get('password')
        device_type = attrs.get('device_type')
        device_id = attrs.get('device_id')

        logger.debug("username: %s, device_type: %s, device_id: %s", username, device_type, device_id)

        if username and password and device_type and device_id:
            device_type = PortalUser.get_device_type(device_type)

            logger.debug("device_type: %s", device_type)

            if device_type is None:
                raise serializers.ValidationError({'device_type': ['Invalid device type.']})

            user = authenticate(username=username, password=password)

            if user:
                if not user.is_active:
                    raise serializers.ValidationError('User account is disabled.')

                setattr(user, "device_type", device_type)
                setattr(user, "device_id", device_id)

                attrs['user'] = user
                return attrs
            else:
                raise serializers.ValidationError('Unable to login with provided credentials.')
        else:
            raise serializers.ValidationError("Must include 'username', 'password', 'device_id' and 'device_type'")


class CtfAuthToken(APIView):
    throttle_classes = ()
    permission_classes = ()
    parser_classes = (parsers.FormParser, parsers.MultiPartParser, parsers.JSONParser,)
    renderer_classes = (renderers.JSONRenderer,)
    serializer_class = AuthTokenSerializer
    model = Token

    def post(self, request):
        serializer = self.serializer_class(data=request.DATA)
        if serializer.is_valid():
            user = serializer.object['user']
            user_json_data = PortalUserSerializer(user, context={'request': request}).data
            user_url = user_json_data.get("url")

            token, created = Token.objects.get_or_create(user=user)
            user.save()  # update user object of device_type and device_id
            return Response(
                {
                    'user': user_url,
                    'token': token.key
                }
            )
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


obtain_auth_token = CtfAuthToken.as_view()