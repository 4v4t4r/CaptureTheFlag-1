package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.Location;
import com.blstream.ctfclient.model.dto.json.RegisterPlayerPositionResponse;

import retrofit.client.Response;
import retrofit.http.Body;
import retrofit.http.DELETE;
import retrofit.http.GET;
import retrofit.http.Header;
import retrofit.http.Headers;
import retrofit.http.PATCH;
import retrofit.http.POST;
import retrofit.http.PUT;
import retrofit.http.Path;

/**
 * Created by mar on 29.04.14.
 */
public interface GameInterface {
    public static final String URL_REQUEST = "/api/games/";

    @Headers({
            "Accept: application/json",
            "Content-type: application/json; charset=utf-8"
    })

    @POST(URL_REQUEST)
    public Game createGame(@Header("Authorization") String token, @Body Game game);

    @GET(URL_REQUEST + "{game_id}/")
    public Game getGame(@Path("game_id") long gameId);

    @PUT(URL_REQUEST + "{game_id}/")
    public void updateGame(@Path("game_id") long gameId, @Body Game game);

    @PATCH(URL_REQUEST + "{game_id}/")
    public void updateFieldInGame(@Path("game_id") long gameId, @Body Game game);

    @DELETE(URL_REQUEST + "{game_id}/")
    public void deleteGame(@Path("game_id") long gameId);

    @POST(URL_REQUEST + "{game_id}/player/")
    public Response addPlayerToGame(@Header("Authorization") String token, @Path("game_id") long gameId);

    @DELETE(URL_REQUEST + "{game_id}/player/")
    public void removePlayerFromGame(@Path("game_id") long gameId);

    @POST(URL_REQUEST + "{game_id}/location/")
    public RegisterPlayerPositionResponse registerPlayerPosition(@Header("Authorization") String token, @Path("game_id") long gameId, @Body Location location);

}