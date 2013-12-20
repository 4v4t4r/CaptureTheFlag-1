from django.test import TestCase
from apps.game.models import Map, Game
from apps.game.serializers import MapSerializer

__author__ = 'mkr'


class GameModelTest(TestCase):
    def test_get(self):
        print "test game get model..."
        # result = Game.objects.get(pk=1)
        # self.assertIsNotNone(result)
        # print "result: ", result


class MapModelTest(TestCase):
    # def test_create(self):
    #     map = Map(
    #         name="Test map",
    #         description="Test map description",
    #         location=None,
    #         radius=5.5,
    #         author="test"
    #     )
    #     data = map.create()
    #     self.assertIsNotNone(data)
    #     print "data: ", data

    def test_get(self):
        data = Map.objects.get(pk="1-n3Rzi8TQ26GmyISGrw8A")
        self.assertIsNotNone(data)
        print "data: ", data

    # def test_delete(self):
    #     data = Map.objects.delete(pk="n3Rzi8TQ26GmyISGrw8A")
    #     self.assertIsNotNone(data)
    #     print "data: ", data


class MapSerializerTest(TestCase):
    def test_serialize(self):
        # print "serialization..."
        # map = Map(name="Test map", description="Test description", location=None, radius=5.50, author="test")
        # serializer = MapSerializer(map)
        # serializer.is_valid()
        # data = serializer.data
        # self.assertIsNotNone(data)
        pass

        # obj = serializer.from_native(data, None)
        # self.assertIsNotNone(obj)
        # self.assertEqual(map.name, obj.name)