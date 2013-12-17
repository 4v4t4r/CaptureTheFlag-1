from rest_framework import serializers
from apps.core.models import PortalUser, Character

__author__ = 'mkr'


class PortalUserSerializer(serializers.ModelSerializer):
    class Meta:
        model = PortalUser
        fields = ('id', 'username', 'first_name', 'last_name', 'email', 'nick')


class CharacterSerializer(serializers.ModelSerializer):
    user = serializers.RelatedField(read_only=False)

    class Meta:
        model = Character
        fields = ('type', 'total_time', 'total_score', 'health', 'level', 'user')