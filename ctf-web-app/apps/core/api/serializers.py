from django.contrib.auth.models import Group
from rest_framework import serializers
from apps.core.models import PortalUser, Character

__author__ = 'mkr'


class PortalUserSerializer(serializers.HyperlinkedModelSerializer):
    def save_object(self, obj, **kwargs):
        super(PortalUserSerializer, self).save_object(obj, **kwargs)
        try:
            player_group = Group.objects.get_by_natural_key("Player")
        except Group.DoesNotExist, e:
            # todo: add logger in this place
            raise e

        print "password: ", obj.password

        if getattr(self, "password_was_set", False):
            print "password: ", obj.password
            obj.set_password(obj.password)
        else:
            print "password was not set"

        player_group.user_set.add(obj)
        obj.save()

    class Meta:
        model = PortalUser
        fields = ('url', 'username', 'password', 'first_name', 'last_name', 'email', 'nick', 'characters')


class CharacterSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Character
        fields = ('url', 'user', 'type', 'total_time', 'total_score', 'health', 'level', 'games')