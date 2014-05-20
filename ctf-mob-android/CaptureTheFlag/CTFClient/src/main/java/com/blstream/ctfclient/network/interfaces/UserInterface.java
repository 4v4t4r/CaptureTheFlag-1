package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.User;

import retrofit.http.GET;
import retrofit.http.Header;
import retrofit.http.Headers;
import retrofit.http.Path;

/**
 * Created by wde on 2014-05-20.
 */
public interface UserInterface {
    public static final String URL_REQUEST = "/api/users/";

    @Headers({
            "Accept: application/json",
            "Content-type: application/json; charset=utf-8"
    })


    @GET(URL_REQUEST + "{id}/")
    public User getUser(@Header("Authorization") String token, @Path("id") long id);
}
