package com.blstream.ctfclient.model.dto.json;

import com.blstream.ctfclient.model.dto.GameSummary;
import com.blstream.ctfclient.model.dto.Marker;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by wde on 2014-05-14.
 */
public class RegisterPlayerPositionResponse {

    @SerializedName("game")
    private GameSummary game;
    private List<Marker> markers;

    public GameSummary getGameSummary() {
        return game;
    }

    public List<Marker> getMarkers() {
        return markers;
    }

    public void setMarkers(List<Marker> markers) {
        this.markers = markers;
    }

    public void setGameSummary(GameSummary game) {
        this.game = game;
    }
}
