import logging
from rest_framework import viewsets
from rest_framework.authentication import SessionAuthentication, BasicAuthentication, OAuth2Authentication
from rest_framework.decorators import api_view, authentication_classes, permission_classes
from rest_framework.mixins import CreateModelMixin, UpdateModelMixin, DestroyModelMixin
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.viewsets import GenericViewSet
from apps.core.api import mixins
from apps.core.api.serializers import characters, users
from apps.core.models import PortalUser, Character

__author__ = 'mkr'

logger = logging.getLogger("root")


@api_view(['GET'])
@authentication_classes((SessionAuthentication, BasicAuthentication, OAuth2Authentication,))
@permission_classes((IsAuthenticated,))
def profile(request):
    user = request.user
    serializer = users.PortalUserSerializer(user)

    if "password" in serializer.data:
        serializer.data.pop("password")

    return Response(serializer.data)


class PortalUserRegistrationViewSet(CreateModelMixin,
                                    GenericViewSet):
    serializer_class = users.PortalUserSerializer
    model = PortalUser

    def get_serializer(self, instance=None, data=None,
                       files=None, many=False, partial=False):

        if data:
            data = data.copy()
            username = data.get(u'username', None)
            if username:
                nick = data.get(u'nick', None)
                if not nick:
                    nick = username
                data[u'nick'] = nick

        logger.debug("JSON request data: %s", data)

        return super(PortalUserRegistrationViewSet, self).get_serializer(instance, data, files, many, partial)


class PortalUserViewSet(mixins.ModelPermissionsMixin,
                        CreateModelMixin,
                        UpdateModelMixin,
                        DestroyModelMixin,
                        mixins.RetrieveModelMixin,
                        mixins.ListModelMixin,
                        GenericViewSet):
    serializer_class = users.PortalUserSerializer
    model = PortalUser
    ignore_fields = ["password"]


class CharacterViewSet(mixins.ModelPermissionsMixin,
                       viewsets.ModelViewSet):
    serializer_class = characters.CharacterSerializer
    model = Character