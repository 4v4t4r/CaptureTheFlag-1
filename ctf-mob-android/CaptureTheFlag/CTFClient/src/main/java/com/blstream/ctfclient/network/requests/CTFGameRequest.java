package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.network.interfaces.GameInterface;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by mar on 25.04.14.
 */
public class CTFGameRequest extends RetrofitSpiceRequest<Game, GameInterface> {

    private Game game;

    public CTFGameRequest(Game game) {
        super(Game.class, GameInterface.class);
        this.game = game;
    }

    @Override
    public Game loadDataFromNetwork() {
        Ln.d("Call web service ");
        return getService().createGame(game);
    }
    public String createCacheKey() {
        return "ctf.game." + game.getName();
    }
}
