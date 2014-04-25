package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.dto.json.TokenResponse;
import com.blstream.ctfclient.network.interfaces.TokenInterface;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by mar on 25.04.14.
 */
public class CTFTokenRequest extends RetrofitSpiceRequest<TokenResponse, TokenInterface> {

    private User user;

    public CTFTokenRequest(User user) {
        super(TokenResponse.class, TokenInterface.class);
        this.user = user;
    }

    @Override
    public TokenResponse loadDataFromNetwork() {
        Ln.d("Call web service ");
        return getService().getUserToken(user);
    }
    public String createCacheKey() {
        return "ctf.token." + user.getUserName();
    }
}
