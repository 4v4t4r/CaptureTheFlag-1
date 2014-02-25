from rest_framework import serializers
from apps.ctf.models import Map

__author__ = 'mkr'


class MapSerializer(serializers.ModelSerializer):
    class Meta:
        model = Map
        fields = ('id', 'name', 'description', 'radius', 'author', 'lat', 'lon')
