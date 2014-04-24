package com.blstream.ctfclient.network.service;

import android.app.Service;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Binder;
import android.os.IBinder;
import android.preference.PreferenceManager;

import java.util.ArrayList;

import retrofit.Callback;
import retrofit.RestAdapter;
import retrofit.RetrofitError;
import retrofit.client.Response;
import retrofit.http.Field;
import retrofit.http.FormUrlEncoded;
import retrofit.http.GET;
import retrofit.http.POST;

/**
 * Created by mar on 24.04.14.
 */
public class AsyncRestService extends Service {

    public interface RestClient {
        @GET("/getRequest")
        void asyncGetArrayList(Callback<ArrayList<String>> response);

        @FormUrlEncoded
        @POST("/postRequest")
        void asyncPostData(@Field("first") String field_1,
                           @Field("second") String field_2,
                           @Field("third") String field_3, Callback<String> callback);
    }

    public interface RestCallback {
        void onGetArrayList(ArrayList<String> result); //on Failure result is null
        void onPostDataResult(String result); //on Failure result is null
    }

    public class RestBinder extends Binder {
        AsyncRestService getService() {
            return AsyncRestService.this;
        }
    }

    private IBinder mBinder = new RestBinder();
    private RestClient mRest;
    private String server = "http:// >_< .com";
    private RestCallback callback;

    @Override
    public IBinder onBind(Intent arg0) {
        return mBinder;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        if (mRest == null) {
            mRest = new RestAdapter.Builder()


           .setServer(server)
                    .setLogLevel(RestAdapter.LogLevel.HEADERS).build()
                    .create(RestClient.class);
        }
    }

    // /RestMethods
    public void setCallback(RestCallback c) {
        callback = c;
    }

    public void unsetCallback() {
        callback = null;
    }

    public void getData() {
        mRest.asyncGetArrayList(new Callback<ArrayList<String>>() {
            RestCallback c = AsyncRestService.this.callback;

            @Override
            public void failure(RetrofitError arg0) {
                c.onGetArrayList(null);
            }

            @Override
            public void success(ArrayList<String> result, Response arg1) {
                c.onGetArrayList(result);
            }
        });
    }

    public void postData(String one, String two, String three) {
        mRest.asyncPostData(one, two, three, new Callback<String>() {
            RestCallback c = AsyncRestService.this.callback;

            @Override
            public void failure(RetrofitError arg0) {
                c.onPostDataResult(null);
            }

            @Override
            public void success(String result, Response arg1) {
                c.onPostDataResult(result);
            }
        });

    }

    public void persistThis(String one, String two, String three) {
        SharedPreferences settings = PreferenceManager
                .getDefaultSharedPreferences(this);
        settings.edit().putString("one", one).putString("two", two)
                .putString("three", three).commit();
    }

    public String[] retrieveThose() {
        String[] result = new String[3];
        SharedPreferences settings = PreferenceManager
                .getDefaultSharedPreferences(this);
        result[0] = settings.getString("one", null);
        result[1] = settings.getString("two", null);
        result[2] = settings.getString("three", null);

        return result;
    }
}