from apps.es import models
from apps.game import constants
from apps.game.serializers import GameSerializer, MapSerializer, ItemSerializer


class Game(models.Model):
    def __init__(self, name=None, description=None, status=constants.GAME_STATUSES.CREATED, start_time=None,
                 max_players=0, type=constants.GAME_TYPES.FRAG_BASED, players=None, items=None, map=None):
        super(Game, self).__init__()
        self.name = name
        self.description = description
        self.status = status
        self.start_time = start_time
        self.max_players = max_players
        self.type = type
        self.players = players
        self.items = items
        self.map = map

    class Meta:
        index = 'ctf'
        doc_type = 'games'
        serializer = GameSerializer()


class Map(models.Model):
    def __init__(self, name=None, description=None, location=None, radius=None, author=None):
        super(Map, self).__init__()
        self.name = name
        self.description = description
        self.location = location
        self.radius = radius
        self.author = author

    class Meta:
        index = 'ctf'
        doc_type = 'maps'
        serializer = MapSerializer()


class Item(models.Model):
    def __init__(self, name=None, description=None, type=None, location=None, value=None):
        self.name = name
        self.description = description
        self.type = type
        self.location = location
        self.value = value

    class Meta:
        index = 'ctf'
        doc_type = 'items'
        serializer = ItemSerializer()
