from django.conf.urls import url, patterns, include
from rest_framework import routers
from apps.ctf.api import views

__author__ = 'mkr'

router = routers.DefaultRouter()
router.register("maps", views.MapViewSet)
router.register("games", views.GameViewSet)
router.register("items", views.ItemViewSet)

urlpatterns = patterns(
    '',
    url(r'^', include(router.urls)),
    url(r'^games/(?P<pk>[0-9]+)/location/$', views.InGameLocation.as_view()),
)