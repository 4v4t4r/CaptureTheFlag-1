from rest_framework import serializers
from apps.ctf.models import Map

__author__ = 'mkr'


class MapSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Map
        fields = ('url', 'author', 'name', 'description', 'radius', 'lat', 'lon', 'games')
        read_only = ('author',)
