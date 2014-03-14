from django.contrib import admin

# Register your models here.
from apps.core.models import PortalUser, Character

admin.site.register(PortalUser)
admin.site.register(Character)