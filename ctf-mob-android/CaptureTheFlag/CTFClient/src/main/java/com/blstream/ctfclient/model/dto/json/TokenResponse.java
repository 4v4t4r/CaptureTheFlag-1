package com.blstream.ctfclient.model.dto.json;

import com.google.gson.annotations.SerializedName;

/**
 * Created by mar on 24.04.14.
 */
public class TokenResponse {
    @SerializedName("token")
    String token;
    @SerializedName("current_user_url")
    String currentUserUrl;

    public String getCurrentUserUrl() {
        return currentUserUrl;
    }

    public void setCurrentUserUrl(String currentUserUrl) {
        this.currentUserUrl = currentUserUrl;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }
}
