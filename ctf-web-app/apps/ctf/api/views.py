import logging
from rest_framework import status
from rest_framework.generics import get_object_or_404
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
        serializer = GeoModelSerializer(data=request.DATA)

        if serializer.is_valid():
            user = request.user
            user.lat = serializer.object.get('lat')
            user.lon = serializer.object.get('lon')
            user.save()

            return Response('ok', status=status.HTTP_204_NO_CONTENT)

        return Response(serializer.errors, status.HTTP_400_BAD_REQUEST)


class JoinToGame(APIView):
    def patch(self, request, pk):
        logger.debug("joining player to selected game...")

        game = get_object_or_404(Game, pk=pk)
        user = request.user
        character = user.get_active_character()

        if character is None:
            return Response(data={"error": "An active character is not defined"}, status=status.HTTP_400_BAD_REQUEST)

        logger.debug("character.user: %s", character.user)

        is_user_already_exist = game.players.filter(user=user).exists()
        logger.debug("is_user_already_exist: %s", is_user_already_exist)

        if is_user_already_exist:
            logger.info("User '%s' already joined into the game '%s'...", user.username, game.name)
        else:
            logger.info("User '%s' in character '%s' is joining into the game '%s'...", user.username, character.type, game.name)
            game.players.add(character)
            game.save()

        return Response(status=status.HTTP_200_OK)

    def delete(self, request, pk):
        logger.debug("removing player from selected game...")

        game = get_object_or_404(Game, pk=pk)
        user = request.user
        character = user.get_active_character()

        if character is None:
            return Response(data={"error": "An active character is not defined"}, status=status.HTTP_400_BAD_REQUEST)

        logger.info("User '%s' in character '%s' is removing from game '%s'...", user.username, character.type, game.name)

        game.players.remove(character)
        game.save()

        return Response(status=status.HTTP_200_OK)