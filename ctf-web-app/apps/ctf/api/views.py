from rest_framework import status
from rest_framework.mixins import CreateModelMixin, UpdateModelMixin, DestroyModelMixin
from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework.viewsets import GenericViewSet
from apps.core.api import mixins
from apps.core.api.serializers import GeoModelSerializer
from apps.ctf.api.serializers.common import ItemSerializer
from apps.ctf.api.serializers.games import GameSerializer
from apps.ctf.api.serializers.maps import MapSerializer
from apps.ctf.models import Map, Game, Item

__author__ = 'mkr'


class MapViewSet(mixins.ModelPermissionsMixin,
                 CreateModelMixin,
                 UpdateModelMixin,
                 DestroyModelMixin,
                 mixins.RetrieveModelMixin,
                 mixins.ListModelMixin,
                 GenericViewSet):
    serializer_class = MapSerializer
    model = Map


class GameViewSet(mixins.ModelPermissionsMixin,
                  CreateModelMixin,
                  UpdateModelMixin,
                  DestroyModelMixin,
                  mixins.RetrieveModelMixin,
                  mixins.ListModelMixin,
                  GenericViewSet):
    serializer_class = GameSerializer
    model = Game


class ItemViewSet(mixins.ModelPermissionsMixin,
                  CreateModelMixin,
                  UpdateModelMixin,
                  DestroyModelMixin,
                  mixins.RetrieveModelMixin,
                  mixins.ListModelMixin,
                  GenericViewSet):
    serializer_class = ItemSerializer
    model = Item


class InGameLocation(APIView):
    def put(self, request, pk, format=None):
        serializer = GeoModelSerializer(data=request.DATA)

        if serializer.is_valid():
            user = request.user
            user.lat = serializer.object.get('lat')
            user.lon = serializer.object.get('lon')
            user.save()

            return Response('', status=status.HTTP_204_NO_CONTENT)

        return Response(serializer.errors, status.HTTP_400_BAD_REQUEST)