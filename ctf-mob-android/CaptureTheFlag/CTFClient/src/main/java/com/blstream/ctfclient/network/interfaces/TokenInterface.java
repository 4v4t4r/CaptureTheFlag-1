package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.dto.json.TokenResponse;

import retrofit.http.Body;
import retrofit.http.Headers;
import retrofit.http.POST;

/**
 * Created by mar on 24.04.14.
 */
public interface TokenInterface {

    public static final String URL_REQUEST = "/token/";

    @Headers({
            "Accept: application/json",
            "Content-type: application/json; charset=utf-8"
    })
    @POST(URL_REQUEST)
    public TokenResponse getUserToken(@Body User user);


}
