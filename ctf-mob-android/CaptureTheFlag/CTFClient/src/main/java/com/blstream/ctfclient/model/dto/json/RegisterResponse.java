package com.blstream.ctfclient.model.dto.json;

import com.blstream.ctfclient.model.dto.Location;
import com.google.gson.annotations.SerializedName;

/**
 * Created by mar on 24.04.14.
 */
public class RegisterResponse {
    String url;
    @SerializedName("username")
    String userName;
    String password;
    @SerializedName("first_name")
    String firstName;
    @SerializedName("last_name")
    String lastName;
    String email;
    String nick;
    @SerializedName("active_character")
    String activeCharacter;
    String[] characters;
    @SerializedName("device_type")
    String deviceType;
    @SerializedName("device_id")
    String deviceId;
    Location location;

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

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getNick() {
        return nick;
    }

    public void setNick(String nick) {
        this.nick = nick;
    }

    public String getActiveCharacter() {
        return activeCharacter;
    }

    public void setActiveCharacter(String activeCharacter) {
        this.activeCharacter = activeCharacter;
    }

    public String[] getCharacters() {
        return characters;
    }

    public void setCharacters(String[] characters) {
        this.characters = characters;
    }

    public String getDeviceType() {
        return deviceType;
    }

    public void setDeviceType(String deviceType) {
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
}
