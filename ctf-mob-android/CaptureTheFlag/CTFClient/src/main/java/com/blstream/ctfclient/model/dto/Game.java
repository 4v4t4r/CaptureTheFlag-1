package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by wde on 29.01.14.
 */
public class Game {
    @SerializedName("id")
    String id;
    @SerializedName("name")
    String name;
    @SerializedName("description")
    String description;
    @SerializedName("status")
    int status;
    @SerializedName("start_time")
    Date start_time;
    @SerializedName("max_players")
    int max_players;
    @SerializedName("type")
    int type;
    @SerializedName("players")
    Character[] players;
    @SerializedName("items")
    Item[] items;
    @SerializedName("map")
    Map map;
    @SerializedName("created_date")
    Date created_date;
    @SerializedName("modified_date")
    Date modified_date;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public int getStatus() {
        return status;
    }

    public void setStatus(int status) {
        this.status = status;
    }

    public Date getStartTime() {
        return start_time;
    }

    public void setStartTime(Date start_time) {
        this.start_time = start_time;
    }

    public int getMaxPlayers() {
        return max_players;
    }

    public void setMaxPlayers(int max_players) {
        this.max_players = max_players;
    }

    public int getType() {
        return type;
    }

    public void setType(int type) {
        this.type = type;
    }

    public Character[] getPlayers() {
        return players;
    }

    public void setPlayers(Character[] players) {
        this.players = players;
    }

    public Item[] getItems() {
        return items;
    }

    public void setItems(Item[] items) {
        this.items = items;
    }

    public Map getMap() {
        return map;
    }

    public void setMap(Map map) {
        this.map = map;
    }

    public Date getCreatedDate() {
        return created_date;
    }

    public void setCreatedDate(Date created_date) {
        this.created_date = created_date;
    }

    public Date getModifiedDate() {
        return modified_date;
    }

    public void setModifiedDate(Date modified_date) {
        this.modified_date = modified_date;
    }
}
