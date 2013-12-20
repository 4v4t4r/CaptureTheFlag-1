from django.conf import settings
from elasticsearch import Elasticsearch
from elasticsearch.exceptions import TransportError
from apps.es.constants import DEFAULT_HOSTS

_settings = getattr(settings, "ELASTIC_SEARCH", None)
_es = None


class Manager(object):
    def __init__(self):
        super(Manager, self).__init__()
        self.model = None

        global _es
        if not _es:
            hosts = getattr(_settings, "hosts", DEFAULT_HOSTS)
            _es = Elasticsearch(hosts=hosts)
        self.es = _es

    def create(self, **kwargs):
        data = self.es.create(index=self.model.Meta.index, doc_type=self.model.Meta.doc_type, body=kwargs)
        return data

    def delete(self, pk):
        data = self.es.delete(index=self.model.Meta.index, doc_type=self.model.Meta.doc_type, id=pk)
        return data

    def get(self, pk):
        try:
            json_data = self.es.get_source(index=self.model.Meta.index, doc_type=self.model.Meta.doc_type, id=pk)
        except TransportError, e:
            if e.status_code == 404:
                raise self.model.DoesNotExist(
                    "No such %s in elastic search where id is '%s'" % (self.model.__name__.lower(), pk))
            raise e

        serializer = self.get_serializer(data=json_data)
        obj = serializer.from_native(json_data, None)
        return obj

    def get_serializer(self, data=None):
        if not hasattr(self.model.Meta, 'serializer'):
            raise ValueError("Serializer class is not defined in object '%s'", self.model.__class__)

        if data:
            serializer = self.model.Meta.serializer.__class__(data=data)
        else:
            serializer = self.model.Meta.serializer.__class__()

        serializer.Meta.model = self.model
        return serializer

    def contribute_to_class(self, model):
        self.model = model


class BaseMode(type):
    def __new__(cls, name, base, kwargs):
        new_class = type.__new__(cls, name, base, kwargs)

        manager = Manager()
        manager.contribute_to_class(new_class)
        setattr(new_class, 'objects', manager)

        return new_class


class Model(object):
    __metaclass__ = BaseMode
    objects = Manager()

    def create(self):
        serializer = self.Meta.serializer.__class__(self)
        json_data = serializer.data
        return self.objects.create(**json_data)

    def delete(self, pk):
        if not pk:
            raise ValueError("Primary key is not defined")
        self.objects.delete(pk)

    def get(self, pk):
        if not pk:
            raise ValueError("Primary key is not defined")
        return self.objects.get(pk)

    class Meta:
        index = None
        doc_type = None
        serializer = None

    class DoesNotExist(BaseException):
        pass