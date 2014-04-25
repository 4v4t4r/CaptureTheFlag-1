package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.dto.json.RegisterResponse;

import retrofit.http.Body;
import retrofit.http.Headers;
import retrofit.http.POST;

/**
 * Created by mar on 24.04.14.
 */
public interface RegisterInterface {

    public static final String URL_REQUEST = "/api/registration/";

    @Headers({
            "Accept: application/json",
            "Content-type: application/json; charset=utf-8"
    })
    @POST(URL_REQUEST)
    public RegisterResponse getUserDetails(@Body User user);


}
