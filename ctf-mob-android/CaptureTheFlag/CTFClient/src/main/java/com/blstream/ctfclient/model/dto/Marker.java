package com.blstream.ctfclient.model.dto;

/**
 * Created by mar on 29.04.14.
 */
public class Marker extends AbstractObject {

    private int type;
    private float distance;
    private Location location;

    public Location getLocation() {
        return location;
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public int getType() {
        return type;
    }

    public void setType(int type) {
        this.type = type;
    }

    public float getDistance() {
        return distance;
    }

    public void setDistance(float distance) {
        this.distance = distance;
    }


}
