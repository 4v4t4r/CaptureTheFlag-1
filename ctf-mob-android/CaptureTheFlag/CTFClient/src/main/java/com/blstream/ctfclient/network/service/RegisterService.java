package com.blstream.ctfclient.network.service;

import com.blstream.ctfclient.model.dto.json.RegisterResponse;
import com.blstream.ctfclient.model.dto.User;

import retrofit.Callback;
import retrofit.http.Body;
import retrofit.http.Headers;
import retrofit.http.POST;

/**
 * Created by mar on 24.04.14.
 */
public interface RegisterService {

    public static final String URL_REQUEST = "/api/registration/";
    @Headers({
            "Accept: application/json",
            "Content-type: application/json; charset=utf-8"
    })
    @POST(URL_REQUEST)
    public void getResponse (@Body User user, Callback<RegisterResponse> response
    );




}
