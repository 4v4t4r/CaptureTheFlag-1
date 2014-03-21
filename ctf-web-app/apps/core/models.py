import logging
import re
from django.contrib.gis.geos import Point
from django.core.mail import send_mail
from model_utils import Choices
from django.utils import timezone
from django.core import validators
from django.utils.translation import ugettext_lazy as _
from django.contrib.auth.models import AbstractUser, AbstractBaseUser, PermissionsMixin, UserManager
from django.db import models, transaction

logger = logging.getLogger('root')


class GeoModelManager(models.Manager):
    """ GeoModel manager class.
    """


class GeoModel(models.Model):
    """ GeoModel object.
    """
    lat = models.FloatField(null=True, blank=True, verbose_name=_("Latitude"))
    lon = models.FloatField(null=True, blank=True, verbose_name=_("Longitude"))

    @property
    def location(self):
        return Point(self.lon, self.lat)

    class Meta:
        abstract = True


class Character(models.Model):
    CHARACTER_TYPES = Choices(
        (0, 'PRIVATE', _('Private')),
        (1, 'MEDIC', _('Medic')),
        (2, 'COMMANDOS', _('Commandos')),
        (3, 'SPY', _('Spy')),
    )

    user = models.ForeignKey('PortalUser', related_name="characters")
    type = models.IntegerField(blank=False, choices=CHARACTER_TYPES, verbose_name=_("Type"))
    total_time = models.IntegerField(blank=False, default=0, verbose_name=_("Total time"))
    total_score = models.IntegerField(blank=False, default=0, verbose_name=_("Total score"))
    health = models.DecimalField(blank=False, max_digits=3, default=1.00, decimal_places=2, verbose_name=_("Health"))
    level = models.IntegerField(blank=False, default=0, verbose_name=_("Level"))

    def __unicode__(self):
        return "%s: %s" % (self.type, self.user)

    class Meta:
        app_label = "core"


class PortalUser(GeoModel, AbstractBaseUser, PermissionsMixin):
    DEVICE_TYPES = Choices(
        (0, 'ANDROID', _("Android")),
        (1, 'WP', _("Windows Phone")),
        (2, 'IOS', _("iOS")),
    )

    username = models.CharField(max_length=100, unique=True, verbose_name=_('username'),
        validators=[
            validators.RegexValidator(re.compile('^[\w.@+-]+$'), _('Enter a valid username.'),
                                      'invalid')
        ])
    email = models.EmailField(_('email address'), max_length=100, blank=False, null=False, unique=True)
    is_staff = models.BooleanField(default=False, verbose_name=_('staff status'))

    nick = models.CharField(blank=False, max_length=100, verbose_name=_("Nick"))

    device_type = models.IntegerField(blank=True, null=True, choices=DEVICE_TYPES, verbose_name=_("Device type"))
    device_id = models.CharField(blank=True, null=True, max_length=255, verbose_name=_("Device type"))

    is_active = models.BooleanField(_('active'), default=True)

    date_joined = models.DateTimeField(
        _('date joined'), default=timezone.now)

    objects = UserManager()

    USERNAME_FIELD = 'username'
    REQUIRED_FIELDS = ['email']

    def email_user(self, subject, message, from_email=None):
        """
        Sends an email to this User.
        """
        send_mail(subject, message, from_email, [self.email])

    @transaction.atomic
    def save(self, *args, **kwargs):
        super(PortalUser, self).save(*args, **kwargs)

        characters = self.characters.all()
        if not characters:
            for character_type in Character.CHARACTER_TYPES:
                character = Character(user=self, type=character_type[0])
                character.save()
            logger.info("characters were saved for user: %s - count: %d", self.username, len(characters))
        else:
            logger.debug("characters already exist in user: %s - count: %d", self.username, len(characters))

    def __unicode__(self):
        return "%s" % self.username

    class Meta(AbstractUser.Meta):
        app_label = "core"
        swappable = 'AUTH_USER_MODEL'
