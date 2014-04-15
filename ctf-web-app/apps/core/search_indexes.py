from haystack import indexes
from apps.core.models import PortalUser
from apps.ctf.models import Item


class GeoModelIndex(indexes.SearchIndex):
    text = indexes.IntegerField(document=True)
    location = indexes.LocationField(null=True, model_attr='location')


class PortalUserIndex(GeoModelIndex, indexes.Indexable):
    def get_model(self):
        return PortalUser


class ItemIndex(GeoModelIndex, indexes.Indexable):
    def get_model(self):
        return Item