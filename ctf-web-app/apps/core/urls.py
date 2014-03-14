from django.conf.urls import patterns, include, url
from apps.core import views

urlpatterns = patterns(
    'apps.core',

    url(r'^$', views.HomePageView.as_view(), name="home_page"),
    url(r'^about/?$', views.AboutPageView.as_view(), name="about_page"),
)
