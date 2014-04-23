import logging
from rest_framework import serializers
from apps.core.api.serializers import CharacterSerializer, LocationField, PortalUserSerializer
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

    def __init__(self, user=None, objects=None):
        self.user = user
        if objects:
            self.to_native(objects)

    def to_native(self, objects):
        if not objects:
            return None

        items = []
        players = []

        for instance in objects:
            object = instance.object
            logger.debug("object: %s", object)
            logger.debug("object type: %s", type(object))

            if isinstance(object, Item):
                serializer = ItemSerializer(object)
                items.append(serializer.data)
            elif isinstance(object, PortalUser) and object.id is not self.user.id:
                serializer = PortalUserSerializer(object)
                players.append(serializer.data)

        self.data = {
            "items": items,
            "players": players
        }