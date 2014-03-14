from rest_framework import serializers
from apps.ctf.models import Game

__author__ = 'mkr'


class GameSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Game
        fields = ('url', 'name', 'description', 'start_time', 'max_players', 'status', 'type', 'map', 'players')