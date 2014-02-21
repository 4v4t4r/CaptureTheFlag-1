from haystack.signals import RealtimeSignalProcessor

__author__ = 'mkr'


class HaystackRealtimeSignalProcessor(RealtimeSignalProcessor):
    def handle_save(self, sender, instance, **kwargs):
        haystack_indexed = getattr(sender, "_haystack_indexed", False)

        if haystack_indexed:
            super(HaystackRealtimeSignalProcessor, self).handle_save(sender, instance, **kwargs)

    def handle_delete(self, sender, instance, **kwargs):
        haystack_indexed = getattr(sender, "_haystack_indexed", False)

        if haystack_indexed:
            super(HaystackRealtimeSignalProcessor, self).handle_delete(sender, instance, **kwargs)
