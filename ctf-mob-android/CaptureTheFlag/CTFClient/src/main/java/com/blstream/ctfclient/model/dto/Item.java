package com.blstream.ctfclient.model.dto;

import com.blstream.ctfclient.model.enums.ItemType;

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


}
