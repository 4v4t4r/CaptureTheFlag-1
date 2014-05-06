package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by mar on 29.04.14.
 */
public class Game extends AbstractObject {

    @SerializedName("start_time")
    private String startTime;
    @SerializedName("max_players")
    private int maxPlayers;
    private GameStaus status;
    private GameType type;
    private String map;
    @SerializedName("visibility_range")
    private float visibilityRange;
    @SerializedName("action_range")
    private float actionRange;
    private List<String> players;
    @SerializedName("invited_users")
    private List<User> invitedUsers;


    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime=startTime;
    }

    public int getMaxPlayers() {
        return maxPlayers;
    }

    public void setMaxPlayers(int maxPlayers) {
        this.maxPlayers = maxPlayers;
    }

    public GameStaus getStatus() {
        return status;
    }

    public void setStatus(GameStaus status) {
        this.status = status;
    }

    public GameType getType() {
        return type;
    }

    public void setType(GameType type) {
        this.type = type;
    }

    public String getMap() {
        return map;
    }

    public void setMap(String map) {
        this.map = map;
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

    public List<User> getInvitedUsers() {
        return invitedUsers;
    }

    public void setInvitedUsers(List<User> invitedUsers) {
        this.invitedUsers = invitedUsers;
    }

    public enum GameStaus {
        @SerializedName("0")
        IN_PROGRESS,
        @SerializedName("1")
        CREATED,
        @SerializedName("2")
        ON_HOLD,
        @SerializedName("3")
        CANCELED
    }

    public enum GameType {
        @SerializedName("0")
        FRAGS,
        @SerializedName("1")
        TIME
    }
}
