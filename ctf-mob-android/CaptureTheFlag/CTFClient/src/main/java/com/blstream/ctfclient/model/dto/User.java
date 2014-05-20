package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Rafal on 08.01.14.
 */
public class User {

    private String url;
    @SerializedName("username")
    private String userName;
    private String email;
    private String password;
    private String nick;
    @SerializedName("device_type")
    private int deviceType;
    @SerializedName("device_id")
    private String deviceId;
    private Location location;
    private TeamType team;
    @SerializedName("current_game_id")
    private int currentGameId;

    public String getUrl() {
        return url;
    }


    public void setUrl(String url) {
        this.url = url;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getNick() {
        return nick;
    }

    public void setNick(String nick) {
        this.nick = nick;
    }

    public int getDeviceType() {
        return deviceType;
    }

    public void setDeviceType(int deviceType) {
        this.deviceType = deviceType;
    }

    public String getDeviceId() {
        return deviceId;
    }

    public void setDeviceId(String deviceId) {
        this.deviceId = deviceId;
    }

    public Location getLocation() {
        return location;
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public TeamType getTeam() {
        return team;
    }

    public void setTeam(TeamType team) {
        this.team = team;
    }

    public int getCurrentGameId() {
        return currentGameId;
    }

    public void setCurrentGameId(int currentGameId) {
        this.currentGameId = currentGameId;
    }

    public enum DeviceType {
        @SerializedName("0")
        ANDROID,
        @SerializedName("1")
        WP,
        @SerializedName("2")
        IOS
    }

    public enum TeamType {
        @SerializedName("0")
        RED_TEAM,
        @SerializedName("1")
        BLUE_TEAM
    }

    @Override
    public String toString() {
        return "User{" +
                "url='" + url + '\'' +
                ", userName='" + userName + '\'' +
                ", email='" + email + '\'' +
                ", password='" + password + '\'' +
                ", nick='" + nick + '\'' +
                ", deviceType=" + deviceType +
                ", deviceId='" + deviceId + '\'' +
                ", location=" + location +
                ", team=" + team +
                ", currentGameId=" + currentGameId +
                '}';
    }
}