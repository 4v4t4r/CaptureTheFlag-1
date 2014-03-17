from haystack import indexes
from apps.core.models import PortalUser


class GeoModelIndex(indexes.SearchIndex, indexes.Indexable):
    id = indexes.IntegerField(document=True)
    location = indexes.LocationField(model_attr='location')


class PortalUserIndex(GeoModelIndex):
    def get_model(self):
        return PortalUser
