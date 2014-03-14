from rest_framework import serializers
from apps.ctf.models import Map

__author__ = 'mkr'


class MapSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Map
        fields = ('url', 'name', 'description', 'radius', 'author', 'lat', 'lon', 'games')
