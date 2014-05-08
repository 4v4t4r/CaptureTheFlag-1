from rest_framework import serializers
from apps.core.api.serializers import LocationField
from apps.ctf.models import Game, Marker

__author__ = 'mkr'


class GameSerializer(serializers.HyperlinkedModelSerializer):
    location = LocationField(required=False)

    class Meta:
        model = Game
        fields = (
            'url', 'name', 'description', 'start_time', 'max_players', 'status', 'type', 'radius', 'location',
            'visibility_range', 'action_range', 'players', 'invited_users', 'items', 'owner', 'last_modified',
            'created',)
        read_only = ('owner', 'last_modified', 'created',)


class MarkerSerializer(serializers.ModelSerializer):
    location = LocationField()

    class Meta:
        model = Marker
        fields = ('marker_type', 'distance', 'url')
