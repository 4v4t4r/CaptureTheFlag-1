package com.blstream.ctfclient.model.dto;

import com.google.android.gms.maps.model.LatLng;

/**
 * Created by mar on 28.04.14.
 */
public class Location {

    private double lat;
    private double lon;

    public Location(float lat, float lon) {
        this.lat = lat;
        this.lon = lon;
    }

    public Location(LatLng latLng) {
        this.lat = latLng.latitude;
        this.lon = latLng.longitude;
    }

    public Location(android.location.Location location) {
        this.lat = location.getLatitude();
        this.lon = location.getLongitude();
    }

    public double getLat() {
        return lat;
    }

    public void setLat(float lat) {
        this.lat = lat;
    }

    public double getLon() {
        return lon;
    }

    public void setLon(float lon) {
        this.lon = lon;
    }

    @Override
    public String toString() {
        return "Location{" +
                "lat=" + lat +
                ", lon=" + lon +
                '}' + toLatLng().toString();
    }

    public LatLng toLatLng() {
        return new LatLng(lat, lon);
    }
}
