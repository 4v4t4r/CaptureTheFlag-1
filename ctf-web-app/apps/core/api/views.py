from rest_framework import viewsets
from rest_framework.decorators import api_view
from rest_framework.mixins import CreateModelMixin, RetrieveModelMixin, UpdateModelMixin, DestroyModelMixin
from rest_framework.permissions import DjangoModelPermissionsOrAnonReadOnly
from rest_framework.response import Response
from rest_framework.viewsets import GenericViewSet
from apps.core.api import mixins
from apps.core.api.serializers import characters, users
from apps.core.models import PortalUser, Character

__author__ = 'mkr'


@api_view(['GET'])
def profile(request):
    # user = request.user
    user = PortalUser.objects.first()
    serializer = users.PortalUserSerializer(user)

    if "password" in serializer.data:
        serializer.data.pop("password")

    return Response(serializer.data)


class PortalUserViewSet(CreateModelMixin,
                        UpdateModelMixin,
                        DestroyModelMixin,
                        mixins.RetrieveModelMixin,
                        mixins.ListModelMixin,
                        GenericViewSet):
    serializer_class = users.PortalUserSerializer
    # permission_classes = (DjangoModelPermissionsOrAnonReadOnly,)
    model = PortalUser
    ignore_fields = ["password"]


class CharacterViewSet(viewsets.ModelViewSet):
    serializer_class = characters.CharacterSerializer
    # permission_classes = (DjangoModelPermissionsOrAnonReadOnly,)
    model = Character