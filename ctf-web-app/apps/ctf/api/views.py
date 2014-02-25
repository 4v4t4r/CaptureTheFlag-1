from rest_framework.mixins import CreateModelMixin, UpdateModelMixin, DestroyModelMixin
from rest_framework.viewsets import GenericViewSet
from apps.core.api import mixins
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