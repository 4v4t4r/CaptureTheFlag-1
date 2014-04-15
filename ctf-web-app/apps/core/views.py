import subprocess
from django.core import urlresolvers
from django.views import generic


class HomePageView(generic.TemplateView):
    template_name = "index.html"

    def get_context_data(self, **kwargs):
        ctx = super(HomePageView, self).get_context_data(**kwargs)
        urls = self._load_urls()
        ctx["urls"] = urls
        ctx["revision"] = self._current_revision()
        return ctx

    def _load_urls(self):
        resolver = urlresolvers.get_resolver(None)
        patterns = sorted(
            [(key, value[0][0][0], '-', '-') for key, value in resolver.reverse_dict.items() if
             isinstance(key, basestring)])
        return patterns

    def _current_revision(self):
        return subprocess.check_output(["git", "rev-parse", "--short", "HEAD"])


class AboutPageView(generic.TemplateView):
    template_name = "about.html"