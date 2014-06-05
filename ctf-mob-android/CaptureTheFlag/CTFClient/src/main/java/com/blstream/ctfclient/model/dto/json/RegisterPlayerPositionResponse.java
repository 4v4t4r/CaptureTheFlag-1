package com.blstream.ctfclient.model.dto.json;

import com.blstream.ctfclient.model.dto.GameSummary;
import com.blstream.ctfclient.model.dto.Item;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by wde on 2014-05-14.
 */
public class RegisterPlayerPositionResponse {

    @SerializedName("game")
    private GameSummary game;

    @SerializedName("markers")
    private List<Item> items;

    public GameSummary getGameSummary() {
        return game;
    }

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }

    public void setGameSummary(GameSummary game) {
        this.game = game;
    }

    @Override
    public String toString() {
        return "RegisterPlayerPositionResponse{" +
                "game=" + ((game == null) ? "null" : game.toString()) +
                ", items=" + ((items == null) ? "null" : items.toString()) +
                '}';
    }
}
