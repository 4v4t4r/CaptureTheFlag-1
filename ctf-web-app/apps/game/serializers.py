from rest_framework import serializers
from apps.es.serializers import ElasticSearchSerializer
from apps.game import constants

__author__ = 'mkr'


class CharacterSerializer(ElasticSearchSerializer):
    from apps.core.models import Character

    user = serializers.CharField(max_length=30, required=True)
    type = serializers.ChoiceField(choices=Character.CHARACTER_TYPES, required=True)
    total_time = serializers.IntegerField(required=True)
    total_score = serializers.IntegerField(required=True)
    health = serializers.DecimalField(max_digits=3, decimal_places=2, required=True)
    level = serializers.IntegerField(required=True)
    # location = None  # todo: add location field


class ItemSerializer(ElasticSearchSerializer):
    user = serializers.CharField(max_length=60, required=True)
    description = serializers.CharField(max_length=100, required=False)
    type = serializers.ChoiceField(choices=constants.ITEM_TYPES, required=True)
    # location = None  # todo: add location field


class MapSerializer(ElasticSearchSerializer):
    name = serializers.CharField(max_length=60, required=True)
    description = serializers.CharField(max_length=100, required=False)
    radius = serializers.DecimalField(max_digits=3, decimal_places=2, required=True)
    author = serializers.CharField(max_length=30, required=True)
    # location = None  # todo: add location field


class GameSerializer(ElasticSearchSerializer):
    name = serializers.CharField(max_length=60, required=True)
    description = serializers.CharField(max_length=100, required=False)
    status = serializers.ChoiceField(choices=constants.GAME_STATUSES, required=True)
    start_time = serializers.DateTimeField(required=True, format="%Y-%m-%d")
    max_players = serializers.IntegerField(required=True)
    type = serializers.ChoiceField(choices=constants.GAME_TYPES, required=True)
    characters = CharacterSerializer()
    items = ItemSerializer()
    map = MapSerializer()