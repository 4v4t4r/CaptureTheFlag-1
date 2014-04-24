package com.blstream.ctfclient.network.service;

import android.util.Log;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.constants.CTFConstants;
import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.dto.json.RegisterResponse;
import com.google.gson.Gson;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;

import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.Response;

/**
 * Created by mar on 24.04.14.
 */
public class CTFService {
    private RestAdapter restAdapter;
    private String urlRequst;
    private RegisterService registerService;
    public static final String KEY_INVALID_USERNAME = "username";
    public static final String KEY_INVALID_EMAIL = "email";
    public static final String KEY_ERROR = "error";
    public static final String KEY_DETAIL = "detail";


    public CTFService() {
        initAdapter();
    }

    public CTFService(String urlRequst) {
        this.urlRequst = urlRequst;
    }

    private void initAdapter() {
        restAdapter = new RestAdapter.Builder()
                .setEndpoint(getURL(""))
                .build();
    }

    public void getRegistrationResponse(String userName, String password, String firstName, String lastName, String email, String nick) {

        User user= new User();
        user.setUserName(userName);
        user.setPassword(password);
        user.setFirstName(firstName);
        user.setLastName(lastName);
        user.setEmail(email);
        user.setNick(nick);
        Gson gson = new Gson();
        String jsonString = gson.toJson(user);
        Log.d(CTF.TAG, "json user: " + jsonString);
        registerService = restAdapter.create(RegisterService.class);

        Callback<RegisterResponse> callback = new Callback<RegisterResponse>() {
            @Override
            public void success(RegisterResponse s, Response response) {
                Log.d("sssssssss", s.getEmail());
            }

            @Override
            public void failure(RetrofitError error) {
                handleError(error);

            }
        };

        registerService.getResponse(user, callback);
    }

    private void handleError(RetrofitError error) {
        int status=  error.getResponse().getStatus();
        Log.d(CTF.TAG,"error code:" +status);
        switch (status) {
            case 400:
            case 404:
            case 422:
            case 401:
                try {
                    String resultString = convertStreamToString(error.getResponse().getBody().in());
                    JSONObject jsonObject = new JSONObject(resultString);

                    String message = null;
                    if (getMessageFromArray(jsonObject, KEY_INVALID_USERNAME) != null) {
                        message = getMessageFromArray(jsonObject, KEY_INVALID_USERNAME);
                    } else if (getMessageFromArray(jsonObject, KEY_INVALID_EMAIL) != null) {
                        message = getMessageFromArray(jsonObject, KEY_INVALID_EMAIL);
                    } else if (getMessage(jsonObject, KEY_ERROR) != null) {
                        message = getMessage(jsonObject, KEY_ERROR);
                    } else if (getMessage(jsonObject, KEY_DETAIL) != null){
                        message = getMessage(jsonObject, KEY_DETAIL);
                    }
                    Log.e(CTF.TAG, message);
                } catch (Exception e) {
                    Log.e(CTF.TAG, "Exception", e);
                }
                break;
            default:

        }
    }


    public String getURL(String params) {
        StringBuilder url = new StringBuilder(CTFConstants.PROTOCOL);
        url.append("://").append(CTFConstants.HOST).append(":").append(CTFConstants.PORT);
        if (!params.startsWith("/")) url.append("/");
        url.append(params);
        return url.toString();
    }

    private static String getMessage(JSONObject jsonObject, String key) {
        String message = null;
        try {
            message = (String) jsonObject.get(key);
        } catch (JSONException e) {
            Log.d(CTF.TAG, "JSONException", e);
        }
        return message;
    }

    private static String getMessageFromArray(JSONObject jsonObject, String key) {
        String message = null;
        try {
            JSONArray jsonArray = (JSONArray) jsonObject.get(key);
            if (jsonArray != null) {
                message = (String) jsonArray.get(0);
            }
        } catch (JSONException e) {
            Log.d(CTF.TAG, "JSONException", e);
        }
        return message;
    }

    public static String extractServerError(java.io.InputStream is) {
        String serverError = null;
        String serverErrorDescription = null;
        try {

            String s = convertStreamToString(is);

            JSONObject messageObject = new JSONObject(s);
            serverError = messageObject.optString("error");
            serverErrorDescription = messageObject.optString("error_description");
            if (serverErrorDescription!=null && !serverErrorDescription.equals("")) {
                return serverErrorDescription;
            } else {
                return serverError;
            }
            //String serverStack = messageObject.getString("stack");
        } catch (Exception e) {
            Log.e("Basemodel", "Error converting RetrofitError server JSON", e);
        }

        return "";
    }

    private static String convertStreamToString(InputStream is) {
        BufferedReader reader = new BufferedReader(new InputStreamReader(is));
        StringBuilder sb = new StringBuilder();

        String line = null;
        try {
            while ((line = reader.readLine()) != null) {
                sb.append(line + "\n");
            }
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                is.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        return sb.toString();
    }
}
