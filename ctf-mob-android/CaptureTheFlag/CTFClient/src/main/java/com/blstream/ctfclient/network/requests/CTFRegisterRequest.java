package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.dto.json.RegisterResponse;
import com.blstream.ctfclient.network.interfaces.RegisterInterface;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by mar on 25.04.14.
 */
public class CTFRegisterRequest  extends RetrofitSpiceRequest<RegisterResponse, RegisterInterface> {

    private User user;

    public CTFRegisterRequest(User user) {
        super(RegisterResponse.class, RegisterInterface.class);
        this.user = user;
    }

    @Override
    public RegisterResponse loadDataFromNetwork() {
        Ln.d("Call web service ");
        return getService().getUserDetails(user);
    }
    public String createCacheKey() {
        return "ctf.user." + user.getUserName();
    }
}
