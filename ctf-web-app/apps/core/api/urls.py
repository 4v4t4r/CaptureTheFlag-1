from django.conf.urls import url, patterns, include
from rest_framework import routers
from apps.core.api import views
from apps.core.api.views import PortalUserViewSet, CharacterViewSet, PortalUserRegistrationViewSet

__author__ = 'mkr'

router = routers.DefaultRouter()
router.register(r'registration', PortalUserRegistrationViewSet)
router.register(r'users', PortalUserViewSet)
router.register(r'characters', CharacterViewSet)

urlpatterns = patterns(
    '',
    url(r'^', include(router.urls)),
    url(r'^profile/?$', view=views.profile),
)