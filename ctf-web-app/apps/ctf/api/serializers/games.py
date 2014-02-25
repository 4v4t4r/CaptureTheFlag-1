from rest_framework import serializers
from apps.core.api.serializers import characters
from apps.ctf.api.serializers import maps
from apps.ctf.api.serializers import common
from apps.ctf.models import Game

__author__ = 'mkr'


class GameSerializer(serializers.ModelSerializer):
    # map = maps.MapSerializer()
    # items = common.ItemSerializer(many=True)
    # players = characters.CharacterSerializer(many=True)

    class Meta:
        model = Game
        fields = ('id', 'name', 'description', 'start_time', 'max_players', 'status', 'type', 'map', 'players', 'items')