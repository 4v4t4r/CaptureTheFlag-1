package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Location;
import com.blstream.ctfclient.model.dto.json.RegisterPlayerPositionResponse;
import com.blstream.ctfclient.network.interfaces.GameInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by wde on 12.05.14.
 */
public class CTFRegisterPlayerPositionRequest extends RetrofitSpiceRequest<RegisterPlayerPositionResponse, GameInterface> {

    private long mGameId;
    private Location mPlayerLocation;

    public CTFRegisterPlayerPositionRequest(long gameId, Location location) {
        super(RegisterPlayerPositionResponse.class, GameInterface.class);
        mGameId = gameId;
        mPlayerLocation = location;
    }

    @Override
    public RegisterPlayerPositionResponse loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().registerPlayerPosition(builder.toString(), mGameId, mPlayerLocation);
    }

    public String createCacheKey() {
        return "CTFRegisterPlayerPositionRequest";
    }
}
