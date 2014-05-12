from geopy.distance import great_circle

__author__ = 'mkr'


def calculate_distance(location_a, location_b):
    return great_circle(
        (location_a.lat, location_a.lon),
        (location_b.lat, location_b.lon)
    ).km
