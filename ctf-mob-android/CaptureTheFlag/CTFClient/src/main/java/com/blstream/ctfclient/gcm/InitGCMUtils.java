package com.blstream.ctfclient.gcm;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.gcm.GoogleCloudMessaging;

import java.io.IOException;


public class InitGCMUtils {

    private static Context mContext;
    private static final String TAG = InitGCMUtils.class.getSimpleName();
    private static final String SENDER_ID = "525859755024";
    private final static int PLAY_SERVICES_RESOLUTION_REQUEST = 9000;

    public static GoogleCloudMessaging gcm;
    static String regid;

    public static void initGCM(Context context) {
        mContext = context;
        if (checkPlayServices()) {
            gcm = GoogleCloudMessaging.getInstance(mContext);
            regid = SharedPreferencesUtils.getDeviceId(mContext);

            if (regid.isEmpty()) {
                registerInBackground();
            }
        } else {
            Log.i(TAG, "No valid Google Play Services APK found.");
        }
    }

    /**
     * Check the device to make sure it has the Google Play Services APK. If
     * it doesn't, display a dialog that allows users to download the APK from
     * the Google Play Store or enable it in the device's system settings.
     */
    private static boolean checkPlayServices() {
        int resultCode = GooglePlayServicesUtil.isGooglePlayServicesAvailable(mContext);
        if (resultCode != ConnectionResult.SUCCESS) {

            if (GooglePlayServicesUtil.isUserRecoverableError(resultCode)) {
//                GooglePlayServicesUtil.getErrorDialog(resultCode, mContext,PLAY_SERVICES_RESOLUTION_REQUEST).show();
            } else {
                Log.i(TAG, "This device is not supported.");
            }
            return false;
        }
        return true;
    }

    /**
     * Sends the registration ID to your server over HTTP, so it can use GCM/HTTP
     * or CCS to send messages to your app. Not needed for this demo since the
     * device sends upstream messages to a server that echoes back the message
     * using the 'from' address in the message.
     */
    private static void sendRegistrationIdToBackend() {
    }

    private static void registerInBackground() {
        new RegistrationTask().execute(null, null, null);
    }

    public static class RegistrationTask extends AsyncTask<Void, Void, String> {


        @Override
        protected String doInBackground(Void... params) {
            String msg = "";
            try {
                if (gcm == null) {
                    gcm = GoogleCloudMessaging.getInstance(mContext);
                }
                regid = gcm.register(SENDER_ID);

                SharedPreferencesUtils.setDeviceId(mContext, regid);

                msg = "Device registered, registration ID=" + regid;

                // You should send the registration ID to your server over HTTP,
                // so it can use GCM/HTTP or CCS to send messages to your app.
                // The request to your server should be authenticated if your app
                // is using accounts.

//                sendRegistrationIdToBackend(); // not needed yet?

            } catch (IOException ex) {
                msg = "Error :" + ex.getMessage();
                // If there is an error, don't just keep trying to register.
                // Require the user to click a button again, or perform
                // exponential back-off.
            }
            return msg;
        }

        @Override
        protected void onPostExecute(String o) {
            Log.d(TAG, o);
        }

    }

}
