package com.blstream.ctfclient.network.requests;

import android.content.SharedPreferences;
import android.preference.PreferenceManager;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;
import com.android.volley.toolbox.StringRequest;
import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.constants.CTFConstants;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Rafal on 17.01.14.
 */
public class CTFRequest extends StringRequest {

    public static final String REQUEST_PARAM_USERS = "api/users/";

    public CTFRequest(int method, String url, Response.Listener<String> listener, Response.ErrorListener errorListener) {
        super(method, url, listener, errorListener);
    }

    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        Map<String, String> headers = new HashMap<>();
        headers.put("Accept", "application/json");
        headers.put("Content-type", "application/json");
        headers.put("Authorization", getAuthorizationString());
        return headers;
    }

    private String getAuthorizationString() {
        StringBuilder stringBuilder = new StringBuilder("Bearer ");
        stringBuilder.append(getToken());
        return stringBuilder.toString();
    }

    private String getToken() {
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(CTF.getStaticApplicationContext());
        return sharedPreferences.getString(CTFConstants.ACCESS_TOKEN_KEY_NAME, "");
    }
}
