package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.Map;

import retrofit.http.GET;
import retrofit.http.Header;
import retrofit.http.Headers;
import retrofit.http.Path;

/**
 * Created by Rafal Zadrozny on 2014-05-05.
 */
public interface MapInterface {

    public static final String URL_REQUEST = "/api/maps/";

    @Headers({
        "Accept: application/json",
        "Content-type: application/json; charset=utf-8"
    })

    @GET(URL_REQUEST + "{map_id}/")
    public Map getMap(@Header("Authorization") String token, @Path("map_id") long mapId);

}
