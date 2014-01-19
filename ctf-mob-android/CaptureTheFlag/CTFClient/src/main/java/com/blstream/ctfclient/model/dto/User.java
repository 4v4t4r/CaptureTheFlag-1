package com.blstream.ctfclient.model.dto;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Rafal on 08.01.14.
 */
public class User {

    @SerializedName("username")
    private String userName;
    @SerializedName("email")
    private String email;
    @SerializedName("password")
    private String password;
    @SerializedName("nick")
    private String nick;
    @SerializedName("first_name")
    private String firstName;
    @SerializedName("last_name")
    private String lastName;
    @SerializedName("location")
    private double[] coordinates;
    @SerializedName("characters")
    private Character[] characters;

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

    public double[] getCoordinates() {
        return coordinates;
    }

    public void setCoordinates(double... coordinates) {
        this.coordinates = coordinates;
    }

    public Character[] getCharacter() {
        return characters;
    }

    public void setCharacter(Character... characters) {
        this.characters = characters;
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
}
