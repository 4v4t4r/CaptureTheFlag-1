package com.blstream.ctfclient;

import android.app.Application;
import android.content.Context;
import android.text.TextUtils;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.Volley;
import com.blstream.ctfclient.constants.CTFConstants;

/**
 * Created by Rafal on 13.01.14.
 */
public class CTF extends Application {

    public final static String TAG = CTF.class.getSimpleName();
    private static Context mContext = null;
    private static RequestQueue requestQueue;
    /**
     * Global request queue for Volley
     */
    private RequestQueue mRequestQueue;

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

    /**
     * @return The Volley Request queue, the queue will be created if it is null
     */
    public RequestQueue getRequestQueue() {
        // lazy initialize the request queue, the queue instance will be
        // created when it is accessed for the first time
        if (mRequestQueue == null) {
            mRequestQueue = Volley.newRequestQueue(mContext);
        }

        return mRequestQueue;
    }

    /**
     * Adds the specified request to the global queue, if tag is specified
     * then it is used else Default TAG is used.
     *
     * @param req
     * @param tag
     */
    public <T> void addToRequestQueue(Request<T> req, String tag) {
        // set the default tag if tag is empty
        req.setTag(TextUtils.isEmpty(tag) ? TAG : tag);

        VolleyLog.d("Adding request to queue: %s", req.getUrl());

        getRequestQueue().add(req);
    }

    /**
     * Adds the specified request to the global queue using the Default TAG.
     *
     * @param req
     */
    public <T> void addToRequestQueue(Request<T> req) {
        // set the default tag if tag is empty
        req.setTag(TAG);

        getRequestQueue().add(req);
    }

    /**
     * Cancels all pending requests by the specified TAG, it is important
     * to specify a TAG so that the pending/ongoing requests can be cancelled.
     *
     * @param tag
     */
    public void cancelPendingRequests(Object tag) {
        if (mRequestQueue != null) {
            mRequestQueue.cancelAll(tag);
        }
    }

    public void cancelPendingRequests() {
        if (mRequestQueue != null) {
            mRequestQueue.cancelAll(TAG);
        }
    }

    public static String getURL(String params) {
        StringBuilder url = new StringBuilder(CTFConstants.PROTOCOL);
        url.append("://").append(CTFConstants.HOST).append(":").append(CTFConstants.PORT);
        if (!params.startsWith("/")) url.append("/");
        url.append(params);
        return url.toString();
    }
}
