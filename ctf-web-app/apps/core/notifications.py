__author__ = 'mkr'


class Notification(object):
    def __init__(self, notification_type, recipients):
        self.type = notification_type
        self.recipients = recipients  # user list

    def send(self):
        pass