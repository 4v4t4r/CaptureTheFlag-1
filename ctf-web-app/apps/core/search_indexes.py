from haystack import indexes
from apps.core.models import PortalUser


class GeoModelIndex(indexes.SearchIndex):
    text = indexes.IntegerField(document=True)
    location = indexes.LocationField(model_attr='location')


class PortalUserIndex(GeoModelIndex, indexes.Indexable):
    def get_model(self):
        return PortalUser
