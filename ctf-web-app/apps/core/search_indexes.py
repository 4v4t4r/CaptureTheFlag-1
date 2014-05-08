from haystack import indexes
from apps.core.models import PortalUser
from apps.ctf.models import Item


class GeoModelIndex(indexes.SearchIndex):
    text = indexes.IntegerField(document=True)
    location = indexes.LocationField(null=True)

    def prepare_location(self, obj):
        return "%s, %s" % (obj.location.lat, obj.location.lon)


class PortalUserIndex(GeoModelIndex, indexes.Indexable):
    game = indexes.IntegerField(null=True)

    def prepare_game(self, obj):
        return obj.current_game_id

    def get_model(self):
        return PortalUser


class ItemIndex(GeoModelIndex, indexes.Indexable):
    game = indexes.IntegerField()

    def prepare_game(self, obj):
        return obj.game.id

    def get_model(self):
        return Item