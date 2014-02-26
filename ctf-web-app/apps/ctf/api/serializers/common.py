from rest_framework import serializers
from apps.ctf.models import Item

__author__ = 'mkr'


class ItemSerializer(serializers.ModelSerializer):
    class Meta:
        model = Item
        fields = ('id', 'name', 'type', 'value', 'description', 'lat', 'lon')