package com.blstream.ctfclient.model.dto.json;

import com.blstream.ctfclient.model.dto.Item;
import com.blstream.ctfclient.model.dto.User;

import java.util.List;

/**
 * Created by mar on 29.04.14.
 */
public class GameResponse {

    private List<Item> items;
    private List<User> players;

    public List<Item> getItems() {
        return items;
    }

    public void setItems(List<Item> items) {
        this.items = items;
    }

    public List<User> getPlayers() {
        return players;
    }

    public void setPlayers(List<User> players) {
        this.players = players;
    }
}
