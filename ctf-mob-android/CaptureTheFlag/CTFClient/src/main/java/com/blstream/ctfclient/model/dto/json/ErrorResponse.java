package com.blstream.ctfclient.model.dto.json;

import com.google.gson.annotations.SerializedName;

/**
 * Created by mar on 24.04.14.
 */
public class ErrorResponse {
    @SerializedName("code")
    private int code;
    @SerializedName(" error")
    private String errorDetails;

    public String getErrorDetails() {
        return errorDetails;
    }

    public void setErrorDetails(String errorDetails) {
        this.errorDetails = errorDetails;
    }

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }
}
