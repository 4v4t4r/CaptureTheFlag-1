package com.blstream.ctfclient.network.service;

import com.blstream.ctfclient.constants.CTFConstants;
import com.blstream.ctfclient.network.interfaces.RegisterInterface;
import com.blstream.ctfclient.network.interfaces.TokenInterface;
import com.octo.android.robospice.retrofit.RetrofitGsonSpiceService;

/**
 * Created by mar on 25.04.14.
 */
public class CTFRequestSpiceService extends RetrofitGsonSpiceService {


    @Override
    public void onCreate() {
        super.onCreate();
        initRetrofitInterfaces();

    }

    private void initRetrofitInterfaces() {
        addRetrofitInterface(RegisterInterface.class);
        addRetrofitInterface(TokenInterface.class);
    }

    @Override
    protected String getServerUrl() {
        return CTFConstants.ENDPOINT;
    }

}