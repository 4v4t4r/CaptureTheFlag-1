from haystack import indexes
from apps.core.models import PortalUser, Location
from apps.ctf.models import Item


class GeoModelIndex(indexes.SearchIndex):
    text = indexes.IntegerField(document=True)
    location = indexes.LocationField(null=True)

    def prepare_location(self, obj):
        print "obj: %s (%s)" % (obj.location, type(obj.location))

        if isinstance(obj.location, Location):
            return "%s,%s" % (obj.location.lat, obj.location.lon)
        return obj.location


class PortalUserIndex(GeoModelIndex, indexes.Indexable):
    def get_model(self):
        return PortalUser


class ItemIndex(GeoModelIndex, indexes.Indexable):
    def get_model(self):
        return Item