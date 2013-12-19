from django.test import TestCase
from apps.game.models import Map
from apps.game.serializers import MapSerializer

__author__ = 'mkr'


class MapSerializerTest(TestCase):
    def test_serialize(self):
        print "serializing..."

        map = Map(name="Test map", description="Test description", location=None, radius=5.50, author="test")
        serializer = MapSerializer(map)
        serializer.is_valid()
        data = serializer.data
        self.assertIsNotNone(data)
        print "data: ", data

        print "is valid: ", serializer.is_valid()

        obj = serializer.from_native(data, None)
        self.assertIsNotNone(obj)
        self.assertEqual(map.name, obj.name)