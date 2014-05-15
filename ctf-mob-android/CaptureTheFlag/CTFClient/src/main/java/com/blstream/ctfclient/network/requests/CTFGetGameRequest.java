package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.network.interfaces.GameInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by wde on 15.05.14.
 */
public class CTFGetGameRequest extends RetrofitSpiceRequest<Game, GameInterface> {

    private long mGameId;

    public CTFGetGameRequest(long gameId) {
        super(Game.class, GameInterface.class);
        mGameId = gameId;
    }

    @Override
    public Game loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().getGame(builder.toString(), mGameId);
    }

    public String createCacheKey() {
        return "CTFGetGameRequest.gameId" + mGameId;
    }
}
