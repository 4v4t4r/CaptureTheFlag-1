package com.blstream.ctfclient.model.dto;

import java.util.List;

/**
 * Created by mar on 29.04.14.
 */
public class Map extends AbstractObject {

    private float radius;
    private String author;
    private float lat;
    private float lon;
    private Location location;
    private List<String> games;


    public Location getLocation() {
        return new Location(lat,lon);
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public List<String> getGames() {
        return games;
    }

    public void setGames(List<String> games) {
        this.games = games;
    }

    public float getRadius() {
        return radius;
    }

    public void setRadius(float radius) {
        this.radius = radius;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }
}
