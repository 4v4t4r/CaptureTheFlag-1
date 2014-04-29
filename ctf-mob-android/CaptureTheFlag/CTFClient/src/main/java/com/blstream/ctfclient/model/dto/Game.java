package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

import java.util.Date;
import java.util.List;

/**
 * Created by mar on 29.04.14.
 */
public class Game extends AbstractObject {

    @SerializedName("start_time")
    private Date startTime;
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


    public Date getStartTime() {
        return startTime;
    }

    public void setStartTime(Date startTime) {
        this.startTime = startTime;
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

    public enum GameStaus{

        IN_PROGRESS,
        CREATED,
        ON_HOLD,
        CANCELED
    }

    public enum GameType {
        FRAGS,
        TIME
    }
}
