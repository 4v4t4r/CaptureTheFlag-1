from django.conf.urls import url, patterns, include
from rest_framework import routers

__author__ = 'mkr'

router = routers.DefaultRouter()

urlpatterns = patterns(
    '',
    url(r'^', include(router.urls)),
)