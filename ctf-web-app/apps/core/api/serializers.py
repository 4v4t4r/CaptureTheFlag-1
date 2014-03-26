import logging
from django.contrib.auth.models import Group
from rest_framework import serializers
from apps.core.models import PortalUser, Character

__author__ = 'mkr'

logger = logging.getLogger('root')


class PortalUserSerializer(serializers.HyperlinkedModelSerializer):
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
            'device_id', 'lat', 'lon')


class CharacterSerializer(serializers.HyperlinkedModelSerializer):
    def save_object(self, obj, **kwargs):
        super(CharacterSerializer, self).save_object(obj, **kwargs)

        if getattr(self, "is_active_was_set", False):
            logger.debug("is_active flag was set")
            if obj.is_active:
                logger.debug("Character '%s' is going to be active...")
                obj.user.set_active_character(obj)

    class Meta:
        model = Character
        fields = ('url', 'user', 'type', 'total_time', 'total_score', 'health', 'level', 'is_active')


class GeoModelSerializer(serializers.Serializer):
    lat = serializers.FloatField()
    lon = serializers.FloatField()