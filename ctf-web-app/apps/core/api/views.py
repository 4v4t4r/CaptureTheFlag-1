import logging
from rest_framework import viewsets
from rest_framework.authentication import SessionAuthentication, TokenAuthentication
from rest_framework.decorators import api_view, authentication_classes, permission_classes, action
from rest_framework.mixins import CreateModelMixin, UpdateModelMixin, DestroyModelMixin
from rest_framework.permissions import IsAuthenticated
from rest_framework.response import Response
from rest_framework.viewsets import GenericViewSet
from apps.core.api import mixins, serializers
from apps.core.models import PortalUser, Character

__author__ = 'mkr'

logger = logging.getLogger("root")


@api_view(['GET'])
@authentication_classes((SessionAuthentication, TokenAuthentication,))
@permission_classes((IsAuthenticated,))
def profile(request):
    user = request.user
    serializer = serializers.PortalUserSerializer(user)

    if "password" in serializer.data:
        serializer.data.pop("password")

    return Response(serializer.data)


class PortalUserRegistrationViewSet(CreateModelMixin, GenericViewSet):
    serializer_class = serializers.PortalUserSerializer
    model = PortalUser

    def get_serializer(self, instance=None, data=None,
                       files=None, many=False, partial=False):
        password_was_set = data and "password" in data

        if data:
            data = data.copy()
            username = data.get(u'username', None)
            if username:
                nick = data.get(u'nick', None)
                if not nick:
                    nick = username
                data[u'nick'] = nick

        logger.debug("JSON request data: %s", data)

        serializer = super(PortalUserRegistrationViewSet, self).get_serializer(instance, data, files, many, partial)
        setattr(serializer, "password_was_set", password_was_set)

        return serializer


class PortalUserViewSet(mixins.ModelPermissionsMixin,
                        CreateModelMixin,
                        UpdateModelMixin,
                        DestroyModelMixin,
                        mixins.RetrieveModelMixin,
                        mixins.ListModelMixin,
                        GenericViewSet):
    serializer_class = serializers.PortalUserSerializer
    model = PortalUser
    ignore_fields = ["password"]

    def get_serializer(self, instance=None, data=None,
                       files=None, many=False, partial=False):
        password_was_set = data and "password" in data

        logger.debug("JSON request data: %s", data)

        serializer = super(PortalUserViewSet, self).get_serializer(instance, data, files, many, partial)
        setattr(serializer, "password_was_set", password_was_set)

        return serializer


class CharacterViewSet(mixins.ModelPermissionsMixin,
                       viewsets.ModelViewSet):
    serializer_class = serializers.CharacterSerializer
    model = Character

    def get_serializer(self, instance=None, data=None,
                       files=None, many=False, partial=False):
        serializer_class = self.get_serializer_class()
        context = self.get_serializer_context()
        serializer = serializer_class(instance, data=data, files=files, many=many, partial=partial, context=context)
        return serializer