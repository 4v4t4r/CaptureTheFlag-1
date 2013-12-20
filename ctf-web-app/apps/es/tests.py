import datetime
from django.test import TestCase
from apps.es.models import Manager


class ManagerTest(TestCase):
    def setUp(self):
        self.manager = Manager()
        self.manager.es.index(
            index="ctf",
            doc_type="games",
            id=1,
            body={
                "name": "BLStreamGames",
                "description": "The most exciting BLStream games ever",
                "status": 1,
                "start_time": "2013-12-18T12:00:00",
                "max_players": 150,
                "type": 0,
                "players": [],
                "items": [],
                "map": {
                    "name": "Szczecin",
                    "description": "Szczecin",
                    "location": {
                        "lat": 40.12,
                        "lon": -71.34
                    },
                    "radius": 1000
                },
                "created_date": datetime.datetime.now()
            }
        )

    def tearDown(self):
        # self.manager.es.delete(index="ctf", doc_type="games", id=1)
        self.manager = None

    def test_connection(self):
        result = self.manager.es.get(index="ctf", doc_type="games", id=1)
        self.assertIsNotNone(result)
