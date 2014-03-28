import logging
from rest_framework import serializers
from apps.core.api.serializers import CharacterSerializer
from apps.core.models import PortalUser
from apps.ctf.models import Item

__author__ = 'mkr'

logger = logging.getLogger("root")


class ItemSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Item
        fields = ('url', 'name', 'type', 'value', 'description', 'lat', 'lon', 'game')


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
                character = object.get_active_character()
                if character is not None:
                    serializer = CharacterSerializer(character)
                    json_data = serializer.data
                    # todo: add filter
                    json_data.pop("is_active")
                    json_data["lat"] = object.lat
                    json_data["lon"] = object.lon
                    players.append(json_data)

        self.data = {
            "items": items,
            "players": players
        }