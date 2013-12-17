from rest_framework import viewsets
from apps.core.api.serializers import characters, users
from apps.core.models import PortalUser, Character

__author__ = 'mkr'


class PortalUserViewSet(viewsets.ModelViewSet):
    serializer_class = users.PortalUserSerializer
    model = PortalUser


class CharacterViewSet(viewsets.ModelViewSet):
    serializer_class = characters.CharacterSerializer
    model = Character