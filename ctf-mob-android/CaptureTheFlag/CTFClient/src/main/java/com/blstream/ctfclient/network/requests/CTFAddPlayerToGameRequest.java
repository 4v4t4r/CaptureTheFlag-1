package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Location;
import com.blstream.ctfclient.network.interfaces.GameInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import retrofit.client.Response;
import roboguice.util.temp.Ln;

/**
 * Created by wde on 12.05.14.
 */
public class CTFAddPlayerToGameRequest extends RetrofitSpiceRequest<Response, GameInterface> {

    private long mGameId;

    public CTFAddPlayerToGameRequest(long gameId) {
        super(Response.class, GameInterface.class);
        mGameId = gameId;
    }

    @Override
    public Response loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().addPlayerToGame(builder.toString(), mGameId);
    }

    public String createCacheKey() {
        return "CTFAddPlayerToGameRequest.gameId" + mGameId;
    }
}
