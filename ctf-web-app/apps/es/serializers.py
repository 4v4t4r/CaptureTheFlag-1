from rest_framework import serializers

__author__ = 'mkr'


class ElasticSearchSerializer(serializers.Serializer):
    def restore_object(self, attrs, instance=None):
        return self._set_attrs(attrs, instance)

    def _set_attrs(self, attrs, instance):
        if instance:
            for field in filter(lambda f: not f.startswith('_'), dir(instance)):
                setattr(instance, field, attrs.get(field, getattr(instance, field)))
            return instance
        return self.Meta.model(attrs)

    class Meta:
        model = None
