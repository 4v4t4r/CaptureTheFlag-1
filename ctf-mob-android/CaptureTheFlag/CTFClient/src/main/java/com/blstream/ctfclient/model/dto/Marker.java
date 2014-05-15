package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

/**
 * Created by mar on 29.04.14.
 */
public class Marker extends AbstractObject {

    private ItemType type;
    private float distance;
    private Location location;

    public Location getLocation() {
        return location;
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public ItemType getType() {
        return type;
    }

    public void setType(ItemType type) {
        this.type = type;
    }

    public float getDistance() {
        return distance;
    }

    public void setDistance(float distance) {
        this.distance = distance;
    }

    @Override
    public String toString() {
        return "Marker{" +
                "type=" + type +
                ", distance=" + distance +
                ", location=" + location +
                '}';
    }

    public enum ItemType {
        @SerializedName("0")
        PLAYER,
        @SerializedName("1")
        PLAYER_WITH_RED_FLAG,
        @SerializedName("2")
        PLAYER_WITH_BLUE_FLAG,
        @SerializedName("3")
        RED_FLAG,
        @SerializedName("4")
        BLUE_FLAG,
        @SerializedName("5")
        RED_BASE,
        @SerializedName("6")
        BLUE_BASE,
        @SerializedName("7")
        RED_BASE_WITH_FLAG,
        @SerializedName("8")
        BLUE_BASE_WITH_FLAG,
        @SerializedName("9")
        FIRST_AID_KIT,
        @SerializedName("10")
        PISTOL,
        @SerializedName("11")
        AMMO
    }
}
