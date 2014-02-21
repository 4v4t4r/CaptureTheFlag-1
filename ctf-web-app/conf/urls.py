from django.conf.urls import patterns, include, url
from django.contrib import admin

admin.autodiscover()

urlpatterns = patterns(
    '',
    url(r'^admin/', include(admin.site.urls)),

    url(r'^', include('apps.core.urls')),
    url(r'^', include('apps.ctf.urls')),

    url(r'^api/', include('apps.core.api.urls', namespace='api_core')),
    url(r'^api/', include('apps.ctf.api.urls', namespace='api_ctf')),

    url(r'^token/', 'apps.core.api.auth.obtain_auth_token'),
)
