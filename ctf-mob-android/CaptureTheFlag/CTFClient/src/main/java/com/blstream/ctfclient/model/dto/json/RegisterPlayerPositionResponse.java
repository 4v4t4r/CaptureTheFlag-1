package com.blstream.ctfclient.model.dto.json;

import com.blstream.ctfclient.model.dto.GameSummary;
import com.blstream.ctfclient.model.dto.Marker;

import java.util.List;

/**
 * Created by wde on 2014-05-14.
 */
public class RegisterPlayerPositionResponse {

    private GameSummary game;
    private List<Marker> markers;

    public GameSummary getGame() {
        return game;
    }

    public List<Marker> getMarkers() {
        return markers;
    }

    public void setMarkers(List<Marker> markers) {
        this.markers = markers;
    }

    public void setGame(GameSummary game) {
        this.game = game;
    }
}
