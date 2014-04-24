import logging
from rest_framework import serializers
from apps.core.api.serializers import LocationField, PortalUserSerializer
from apps.core.models import PortalUser
from apps.ctf.models import Item

__author__ = 'mkr'

logger = logging.getLogger("root")


class ItemSerializer(serializers.HyperlinkedModelSerializer):
    location = LocationField(required=False)

    class Meta:
        model = Item
        fields = ('url', 'name', 'type', 'value', 'description', 'location', 'game')


class NeighbourSerializer(object):
    data = None

    def __init__(self, user=None, objects=None, context=None):
        self.user = user
        self.context = context

        if objects:
            self.to_native(objects)

    def to_native(self, objects):
        if not objects:
            return None

        items = []
        players = []

        for instance in objects:
            distance_in_meters = instance.distance.m if instance.distance is not None else None
            obj = instance.object
            logger.debug("object: %s, distance: %s m", obj, distance_in_meters)
            logger.debug("object type: %s", type(obj))

            if isinstance(obj, Item):
                serializer = ItemSerializer(obj, context=self.context)
                json_data = serializer.data
                json_data["distance"] = distance_in_meters
                items.append(json_data)
            elif isinstance(obj, PortalUser) and obj.id is not self.user.id:
                serializer = PortalUserSerializer(obj, context=self.context)
                json_data = serializer.data
                json_data.pop("password")  # removing password from json
                json_data["distance"] = distance_in_meters
                players.append(json_data)

        self.data = {
            "items": items,
            "players": players
        }