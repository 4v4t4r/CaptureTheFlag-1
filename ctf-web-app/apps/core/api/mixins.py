import warnings
from django.http import Http404
from rest_framework.response import Response

__author__ = 'mkr'


class RetrieveModelMixin(object):
    """
    Retrieve a model instance.
    """

    ignore_fields = []
    object = None

    def retrieve(self, request, *args, **kwargs):
        self.object = self.get_object()
        serializer = self.get_serializer(self.object)

        for ignore_field in self.ignore_fields:
            if ignore_field in serializer.data:
                serializer.data.pop(ignore_field)

        return Response(serializer.data)


class ListModelMixin(object):
    """
    List a queryset.
    """
    empty_error = "Empty list and '%(class_name)s.allow_empty' is False."

    ignore_fields = []

    def list(self, request, *args, **kwargs):
        self.object_list = self.filter_queryset(self.get_queryset())

        # Default is to allow empty querysets.  This can be altered by setting
        # `.allow_empty = False`, to raise 404 errors on empty querysets.
        if not self.allow_empty and not self.object_list:
            warnings.warn(
                'The `allow_empty` parameter is due to be deprecated. '
                'To use `allow_empty=False` style behavior, You should override '
                '`get_queryset()` and explicitly raise a 404 on empty querysets.',
                PendingDeprecationWarning
            )
            class_name = self.__class__.__name__
            error_msg = self.empty_error % {'class_name': class_name}
            raise Http404(error_msg)

        # Switch between paginated or standard style responses
        page = self.paginate_queryset(self.object_list)
        if page is not None:
            serializer = self.get_pagination_serializer(page)
        else:
            serializer = self.get_serializer(self.object_list, many=True)

        self._remove_ignored_fields(serializer.data)

        return Response(serializer.data)

    def _remove_ignored_fields(self, data):
        if not data:
            return

        for m in data:
            for ignore_field in self.ignore_fields:
                if ignore_field in m:
                    m.pop(ignore_field)


