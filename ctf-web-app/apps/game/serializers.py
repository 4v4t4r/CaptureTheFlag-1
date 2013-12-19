from rest_framework import serializers
from apps.core.models import Character
from apps.game.models import Game, Item, Map

__author__ = 'mkr'


class ElasticSearchSerializer(serializers.Serializer):
    def restore_object(self, attrs, instance=None):
        return self._set_attrs(attrs, instance)

    def _set_attrs(self, attrs, instance):
        if instance:
            for field in filter(lambda f: not f.startswith('_'), dir(instance)):
                setattr(instance, field, attrs.get(field, getattr(instance, field)))
            return instance
        return self.Meta.model(attrs)

    class Meta:
        model = None


class CharacterSerializer(ElasticSearchSerializer):
    user = serializers.CharField(max_length=30, required=True)
    type = serializers.ChoiceField(choices=Character.CHARACTER_TYPES, required=True)
    total_time = serializers.IntegerField(required=True)
    total_score = serializers.IntegerField(required=True)
    health = serializers.DecimalField(max_digits=3, decimal_places=2, required=True)
    level = serializers.IntegerField(required=True)
    location = None  # todo: add location field

    class Meta:
        model = Character


class ItemSerializer(ElasticSearchSerializer):
    user = serializers.CharField(max_length=60, required=True)
    description = serializers.CharField(max_length=100, required=False)
    type = serializers.ChoiceField(choices=Item.TYPE, required=True)
    location = None  # todo: add location field

    class Meta:
        model = Item


class MapSerializer(ElasticSearchSerializer):
    name = serializers.CharField(max_length=60, required=True)
    description = serializers.CharField(max_length=100, required=False)
    radius = serializers.DecimalField(max_digits=3, decimal_places=2, required=True)
    author = serializers.CharField(max_length=30, required=True)
    location = None  # todo: add location field

    class Meta:
        model = Map


class GameSerializer(ElasticSearchSerializer):
    name = serializers.CharField(max_length=60, required=True)
    description = serializers.CharField(max_length=100, required=False)
    status = serializers.ChoiceField(choices=Game.STATUS, required=True)
    start_time = serializers.DateTimeField(required=True, format="%Y-%m-%d")
    max_players = serializers.IntegerField(required=True)
    type = serializers.ChoiceField(choices=Game.TYPE, required=True)
    characters = CharacterSerializer()
    items = ItemSerializer()
    map = MapSerializer()

    class Meta:
        model = Game
