package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.Game;

import retrofit.http.GET;
import retrofit.http.Header;
import retrofit.http.Headers;

/**
 * Created by Rafał Zadrożny on 2014-04-30.
 */
public interface GamesInterface {

    public static final String URL_REQUEST = "/api/games/";

    @Headers({
        "Accept: application/json",
        "Content-type: application/json; charset=utf-8"
    })
    @GET(URL_REQUEST)
    public Game[] getGames(@Header("Authorization") String token);

}
