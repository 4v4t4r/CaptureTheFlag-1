from rest_framework import serializers
from apps.core.models import PortalUser, Character

__author__ = 'mkr'


class CharacterSerializer(serializers.ModelSerializer):
    class Meta:
        model = Character
        fields = ('type', 'total_time', 'total_score', 'health', 'level')


class PortalUserSerializer(serializers.ModelSerializer):
    characters = CharacterSerializer(read_only=True)

    class Meta:
        model = PortalUser
        fields = ('id', 'username', 'first_name', 'last_name', 'email', 'nick', 'characters')