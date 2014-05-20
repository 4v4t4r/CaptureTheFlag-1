package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.network.interfaces.UserInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by mar on 25.04.14.
 */
public class CTFGetUserRequest extends RetrofitSpiceRequest<User, UserInterface> {

    private long userId;

    public CTFGetUserRequest(long id) {
        super(User.class, UserInterface.class);
        userId = id;
    }

    @Override
    public User loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().getUser(builder.toString(), userId);
    }

    public String createCacheKey() {
        return "CTFGetUserRequest." + userId;
    }
}
