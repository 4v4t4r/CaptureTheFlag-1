package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.network.interfaces.GameInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by mar on 25.04.14.
 */
public class CTFCreateGameRequest extends RetrofitSpiceRequest<Game, GameInterface> {

    private Game game;

    public CTFCreateGameRequest(Game game) {
        super(Game.class, GameInterface.class);
        this.game = game;
    }

    @Override
    public Game loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().createGame(builder.toString(), game);
    }
    public String createCacheKey() {
        return "ctf.game." + game.getName();
    }
}
