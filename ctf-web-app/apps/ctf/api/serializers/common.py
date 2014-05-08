import logging
from rest_framework import serializers
from apps.core.api.serializers import LocationField
from apps.ctf.models import Item

__author__ = 'mkr'

logger = logging.getLogger("root")


class ItemSerializer(serializers.HyperlinkedModelSerializer):
    location = LocationField(required=False)

    class Meta:
        model = Item
        fields = ('url', 'name', 'type', 'value', 'description', 'location', 'game')