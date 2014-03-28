import logging
from rest_framework import status
from rest_framework.generics import get_object_or_404
from rest_framework.mixins import CreateModelMixin, UpdateModelMixin, DestroyModelMixin
from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework.viewsets import GenericViewSet
from apps.core.api import mixins
from apps.core.api.serializers import GeoModelSerializer
from apps.ctf.api.serializers.common import ItemSerializer, NeighbourSerializer
from apps.ctf.api.serializers.games import GameSerializer
from apps.ctf.api.serializers.maps import MapSerializer
from apps.ctf.models import Map, Game, Item

__author__ = 'mkr'

logger = logging.getLogger("root")


class MapViewSet(mixins.ModelPermissionsMixin,
                 CreateModelMixin,
                 UpdateModelMixin,
                 DestroyModelMixin,
                 mixins.RetrieveModelMixin,
                 mixins.ListModelMixin,
                 GenericViewSet):
    serializer_class = MapSerializer
    model = Map

    def pre_save(self, obj):
        user = self.request.user
        setattr(obj, "author", user)


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
        game = get_object_or_404(Game, pk=pk)
        serializer = GeoModelSerializer(data=request.DATA)

        if serializer.is_valid():
            user = request.user
            user.lat = serializer.object.get('lat')
            user.lon = serializer.object.get('lon')
            user.save()

            neighbours = game.get_neighbours(user)
            logger.debug("neighbours size: %d", len(neighbours))
            logger.debug("neighbours: %s", neighbours)

            neighbour_serializer = NeighbourSerializer(user, neighbours)
            data = neighbour_serializer.data
            logger.debug("data: %s", data)

            return Response(data=data, status=status.HTTP_200_OK)

        return Response(serializer.errors, status.HTTP_400_BAD_REQUEST)


class JoinToGame(APIView):
    def post(self, request, pk):
        logger.debug("joining player to selected game...")

        game = get_object_or_404(Game, pk=pk)
        user = request.user

        try:
            character = game.add_player(user)
        except AssertionError, e:
            # todo: add error code
            return Response(data={"error": e.message}, status=status.HTTP_400_BAD_REQUEST)
        except Exception, e:
            # todo: add error code
            return Response(data={"error": e.message}, status=status.HTTP_400_BAD_REQUEST)
        else:
            logger.info("Player '%s' in character is in game '%s'", user.username, character.type, game.name)
            return Response(status=status.HTTP_200_OK)

    def delete(self, request, pk):
        logger.debug("removing player from selected game...")

        game = get_object_or_404(Game, pk=pk)
        user = request.user

        try:
            character = game.remove_player(user)
        except AssertionError, e:
            return Response(data={"error": e.message}, status=status.HTTP_400_BAD_REQUEST)
        else:
            logger.info("Player '%s' in character is no longer in game '%s'", user.username, character.type, game.name)
            return Response(status=status.HTTP_200_OK)