package com.blstream.ctfclient;

import android.app.Application;
import android.content.Context;

import com.blstream.ctfclient.constants.CTFConstants;

/**
 * Created by Rafal on 13.01.14.
 */
public class CTF extends Application {

    public final static String TAG = CTF.class.getSimpleName();
    private static Context mContext = null;

    /**
     * A singleton instance of the application class for easy access in other places
     */
    private static CTF sInstance;

    public void onCreate() {
        mContext = getApplicationContext();
        // initialize the singleton
        sInstance = this;
    }

    public static Context getStaticApplicationContext() {
        return mContext;
    }

    /**
     * @return ApplicationController singleton instance
     */
    public static synchronized CTF getInstance() {
        return sInstance;
    }


    public static String getURL(String params) {
        StringBuilder url = new StringBuilder(CTFConstants.PROTOCOL);
        url.append("://").append(CTFConstants.HOST).append(":").append(CTFConstants.PORT);
        if (!params.startsWith("/")) url.append("/");
        url.append(params);
        return url.toString();
    }
}
