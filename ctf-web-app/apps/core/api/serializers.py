import logging
from django.contrib.auth.models import Group
from rest_framework import serializers
from apps.core.models import PortalUser, Character, Location

__author__ = 'mkr'

logger = logging.getLogger('root')


class LocationField(serializers.WritableField):
    def to_native(self, data):
        logger.debug("to_native: data: %s (%s)", data, type(data))

        if not data:
            return None

        if isinstance(data, Location):
            lat, lon = (data.lat, data.lon)
        elif isinstance(data, basestring):
            lat, lon = [col.strip() for col in data.split(",")]
        else:
            raise serializers.validators.ValidationError("Wrong native value")
        return {
            "lat": lat,
            "lon": lon
        }

    def from_native(self, data):
        logger.debug("from_native: data: %s (%s)", data, type(data))

        if not data:
            return None

        if isinstance(data, dict):
            lat = data.get("lat")
            lon = data.get("lon")
        elif isinstance(data, basestring):
            lat, lon = [col.strip() for col in data.split(",")]
        else:
            raise serializers.validators.ValidationError("Wrong native value")

        return Location(lat, lon)


class PortalUserSerializer(serializers.HyperlinkedModelSerializer):
    location = LocationField(required=False)

    def save_object(self, obj, **kwargs):
        super(PortalUserSerializer, self).save_object(obj, **kwargs)
        try:
            player_group = Group.objects.get_by_natural_key("Player")
        except Group.DoesNotExist, e:
            # todo: add logger in this place
            raise e

        if getattr(self, "password_was_set", False):
            logger.debug("password was set")
            obj.set_password(obj.password)
        else:
            logger.debug("password was not set")

        player_group.user_set.add(obj)
        obj.save()

    class Meta:
        model = PortalUser
        fields = (
            'url', 'username', 'password', 'first_name', 'last_name', 'email', 'nick', 'characters', 'device_type',
            'device_id', 'location')


class CharacterSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Character
        fields = ('url', 'user', 'type', 'total_time', 'total_score', 'health', 'level')


class GeoModelSerializer(serializers.Serializer):
    lat = serializers.FloatField()
    lon = serializers.FloatField()