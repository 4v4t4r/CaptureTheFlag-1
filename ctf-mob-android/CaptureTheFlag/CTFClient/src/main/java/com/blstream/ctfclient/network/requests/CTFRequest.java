package com.blstream.ctfclient.network.requests;

import android.content.SharedPreferences;
import android.preference.PreferenceManager;

import com.android.volley.AuthFailureError;
import com.android.volley.Response;
import com.android.volley.toolbox.JsonObjectRequest;
import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.constants.CTFConstants;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Rafal on 17.01.14.
 */
public class CTFRequest extends JsonObjectRequest {

    public static final String PARAM_USERS = "api/users/";

    public CTFRequest(int method, String url, JSONObject jsonRequest, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
        super(method, url, jsonRequest, listener, errorListener);
    }

    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(CTF.getStaticApplicationContext());
        String token = sharedPreferences.getString(CTFConstants.ACCESS_TOKEN_KEY_NAME, "");
        StringBuilder stringBuilder = new StringBuilder("Bearer ");
        stringBuilder.append(token);
        Map<String, String> headers = new HashMap<>();
        headers.put("Accept", "application/json");
        headers.put("Content-type", "application/json; charset=utf-8");
        headers.put("Authorization", stringBuilder.toString());
        return headers;
    }
}
