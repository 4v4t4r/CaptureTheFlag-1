from model_utils import Choices
from django.utils.translation import ugettext_lazy as _
from django.contrib.auth.models import AbstractUser, UserManager
from django.db import models


class PortalUserManager(UserManager):
    pass


class PortalUser(AbstractUser):
    nick = models.CharField(blank=False, max_length=100, verbose_name=_("Nick"))
    AbstractUser._meta.get_field("email").blank = False
    AbstractUser._meta.get_field("email").null = False

    objects = PortalUserManager()

    class Meta:
        app_label = "core"
        # db_table = "portal_users"
        swappable = 'AUTH_USER_MODEL'


class Character(models.Model):
    CHARACTER_TYPES = Choices(
        (0, 'PRIVATE',  _('Private')),
        (1, 'MEDIC', _('Medic')),
        (2, 'COMMANDOS', _('Commandos')),
    )

    user = models.ForeignKey("PortalUser", related_name="characters")
    type = models.IntegerField(blank=False, choices=CHARACTER_TYPES, verbose_name=_("Type"))
    total_time = models.IntegerField(blank=False, default=0, verbose_name=_("Total time"))
    total_score = models.IntegerField(blank=False, default=0, verbose_name=_("Total score"))
    health = models.DecimalField(blank=False, max_digits=3, default=1.00, decimal_places=2, verbose_name=_("Health"))
    level = models.IntegerField(blank=False, default=0, verbose_name=_("Level"))

    class Meta:
        app_label = "core"
        # db_table = "characters"