package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by mar on 29.04.14.
 * <p/>
 * Docs:
 * https://github.com/blstream/CaptureTheFlag/blob/master/ctf-web-app/docs/models.rst#model-game
 */
public class Game extends AbstractObject {

    @SerializedName("start_time")
    private String startTime;
    @SerializedName("max_players")
    private int maxPlayers;
    private GameStatus status;
    private GameType type;
    private int radius;
    private Location location;
    @SerializedName("visibility_range")
    private float visibilityRange;
    @SerializedName("action_range")
    private float actionRange;
    private List<String> players;
    @SerializedName("invited_users")
    private List<String> invitedUsers;
    private List<String> items;
    private String owner;
    @SerializedName("last_modified")
    private String lastModified;
    private String created;

    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime = startTime;
    }

    public int getMaxPlayers() {
        return maxPlayers;
    }

    public void setMaxPlayers(int maxPlayers) {
        this.maxPlayers = maxPlayers;
    }

    public GameStatus getStatus() {
        return status;
    }

    public void setStatus(GameStatus status) {
        this.status = status;
    }

    public GameType getType() {
        return type;
    }

    public void setType(GameType type) {
        this.type = type;
    }

    public Location getLocation() {
        return location;
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public int getRadius() {
        return radius;
    }

    public void setRadius(int radius) {
        this.radius = radius;
    }

    public float getVisibilityRange() {
        return visibilityRange;
    }

    public void setVisibilityRange(float visibilityRange) {
        this.visibilityRange = visibilityRange;
    }

    public float getActionRange() {
        return actionRange;
    }

    public void setActionRange(float actionRange) {
        this.actionRange = actionRange;
    }

    public List<String> getPlayers() {
        return players;
    }

    public void setPlayers(List<String> players) {
        this.players = players;
    }

    public List<String> getInvitedUsers() {
        return invitedUsers;
    }

    public void setInvitedUsers(List<String> invitedUsers) {
        this.invitedUsers = invitedUsers;
    }

    public String getCreated() {
        return created;
    }

    public void setCreated(String created) {
        this.created = created;
    }

    public List<String> getItems() {
        return items;
    }

    public void setItems(List<String> items) {
        this.items = items;
    }

    public String getOwner() {
        return owner;
    }

    public void setOwner(String owner) {
        this.owner = owner;
    }

    public String getLastModified() {
        return lastModified;
    }

    public void setLastModified(String lastModified) {
        this.lastModified = lastModified;
    }

    public enum GameStatus {
        @SerializedName("0")
        IN_PROGRESS,
        @SerializedName("1")
        CREATED,
        @SerializedName("2")
        ON_HOLD,
        @SerializedName("3")
        CANCELED,
        @SerializedName("4")
        FINISHED
    }

    public enum GameType {
        @SerializedName("0")
        FRAGS,
        @SerializedName("1")
        TIME
    }
}
