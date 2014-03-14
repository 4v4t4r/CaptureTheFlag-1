from rest_framework import serializers
from apps.ctf.models import Item

__author__ = 'mkr'


class ItemSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Item
        fields = ('url', 'name', 'type', 'value', 'description', 'lat', 'lon', 'games')