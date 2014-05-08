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
    private String[] characters;
    @SerializedName("active_character")
    private String activeCharacter;

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

    public String[] getCharacters() {
        return characters;
    }

    public void setCharacters(String[] characters) {
        this.characters = characters;
    }

    public String getActiveCharacter() {
        return activeCharacter;
    }

    public void setActiveCharacter(String activeCharacter) {
        this.activeCharacter = activeCharacter;
    }

    public enum DeviceType {
        ANDROID(0),
        WP(1),
        IOS(2);

        DeviceType(int i) {
            this.type = i;
        }

        private int type;

        public int getNumericType() {
            return type;
        }
    }

    public enum CharacterType {
        PRIVATE,
        MEDIC,
        COMMANDOS,
        SPY
    }
}