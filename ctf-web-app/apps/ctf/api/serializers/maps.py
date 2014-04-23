from rest_framework import serializers
from apps.core.api.serializers import LocationField
from apps.ctf.models import Map

__author__ = 'mkr'


class MapSerializer(serializers.HyperlinkedModelSerializer):
    location = LocationField(required=False)

    class Meta:
        model = Map
        fields = ('url', 'author', 'name', 'description', 'radius', 'location', 'games')
        read_only = ('author',)
