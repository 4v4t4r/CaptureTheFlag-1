package com.blstream.ctfclient.model.dto;

/**
 * Created by mar on 29.04.14.
 */
public class Item extends AbstractObject {

    private ItemType type;
    private float value;
    private float lat;
    private float lon;
    private Location location;
    private String game;


    public Location getLocation() {
        return new Location(lat, lon);
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public enum ItemType {
        FLAG_RED,
        FLAG_BLUE,
        BASE_RED,
        BASE_BLUE,
        AID_KIT,
        PISTOL,
        AMMO
    }
}
