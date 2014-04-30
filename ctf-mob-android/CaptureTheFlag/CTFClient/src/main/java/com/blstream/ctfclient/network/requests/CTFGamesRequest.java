package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.network.interfaces.GamesInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by Rafał Zadrożny on 2014-04-30.
 */
public class CTFGamesRequest extends RetrofitSpiceRequest<Game[], GamesInterface> {

    public CTFGamesRequest() {
        super(Game[].class, GamesInterface.class);
    }

    @Override
    public Game[] loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().getGames( builder.toString() );
    }

    public String createCacheKey() {
        return "ctf.token." + SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext());
    }
}