package com.blstream.ctfclient.gcm;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.IBinder;
import android.util.Log;

/**
 * Created by marcin on 05.03.14.
 */
public class GCMService extends Service {
    private String TAG = "StartServiceAtBoot: " +GCMService.class.getSimpleName();

    private Context mContext;


    public IBinder onBind(Intent intent) {
        return null;
    }

    @Override
    public void onCreate() {
        Log.d(TAG, "Created");
        mContext = this;
        InitGCMUtils.initGCM(mContext);

    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        Log.d(TAG, "onStartCommand()");

        // We want this service to continue running until it is explicitly
        // stopped, so return sticky.
        return START_STICKY;
    }

	    /*
         * In Android 2.0 and later, onStart() is depreciated.  Use
	     * onStartCommand() instead, or compile against API Level 5 and
	     * use both.
	     * http://android-developers.blogspot.com/2010/02/service-api-changes-starting-with.html
	    	@Override
	    	public void onStart(Intent intent, int startId)
	    	{
	    		Log.v("StartServiceAtBoot", "StartAtBootService -- onStart()");
	    	}
	     */

    @Override
    public void onDestroy() {
        Log.d(TAG, "Destroyed");
    }

}