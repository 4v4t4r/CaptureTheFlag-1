package com.blstream.ctfclient.network.interfaces;

import com.blstream.ctfclient.model.dto.Item;

import retrofit.http.Body;
import retrofit.http.Header;
import retrofit.http.Headers;
import retrofit.http.POST;

/**
 * Created by wde on 2014-05-21.
 */
public interface ItemInterface {
    public static final String URL_REQUEST = "/api/items/";

    @Headers({
            "Accept: application/json",
            "Content-type: application/json; charset=utf-8"
    })

    @POST(URL_REQUEST)
    public Item createItem(@Header("Authorization") String token, @Body Item item);
}