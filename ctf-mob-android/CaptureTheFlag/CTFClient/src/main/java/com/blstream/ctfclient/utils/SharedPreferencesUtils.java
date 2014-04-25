package com.blstream.ctfclient.utils;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.preference.PreferenceManager;
import android.util.Log;

import com.blstream.ctfclient.activities.MainActivity;


public class SharedPreferencesUtils {

    private static final String TAG =SharedPreferencesUtils.class.getSimpleName();

    public static final String FACEBOOK_TOKEN = "com.blstream.ctfclient.utils.facebook.token";
    private static final String TOKEN = "com.blstream.ctfclient.utils.api.token";

    //CGM
    public static final String CGM_REG_ID = "com.blstream.ctfclient.utils.cgm.registration_id";
    private static final String PROPERTY_APP_VERSION = "com.blstream.ctfclient.utils.cgm.app.version";



    public static String getFaceBookToken(Context ctx) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(ctx);
        return prefs.getString(FACEBOOK_TOKEN, "");
    }

    public static void setFaceBookToken(Context ctx, String facebookToken) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(ctx);
        SharedPreferences.Editor editor = prefs.edit();
        editor.putString(FACEBOOK_TOKEN, facebookToken);
        editor.commit();
    }

//    /**
//     * Gets the current registration ID for application on GCM service.
//     * <p>
//     * If result is empty, the app needs to register.
//     *
//     * @return registration ID, or empty string if there is no existing
//     *         registration ID.
//     */
//    public static String getPushToken(Context ctx) {
//        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(ctx);
//        return prefs.getString(PUSH_TOKEN, "");
//    }
//    /**
//     * Set the registration ID for application on GCM service.
//     */
//    public static void setPushToken(Context ctx, String pushToken) {
//        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(ctx);
//        SharedPreferences.Editor editor = prefs.edit();
//        editor.putString(PUSH_TOKEN, pushToken);
//        editor.commit();
//    }


    public static void setToken(Context ctx, String apiToken) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(ctx);
        SharedPreferences.Editor editor = prefs.edit();
        editor.putString(TOKEN, apiToken);
        editor.commit();

    }

    public static String getToken(Context ctx) {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(ctx);
        return prefs.getString(TOKEN, null);
    }

    /**
     * @return Application's {@code SharedPreferences}.
     */
    private static SharedPreferences getGCMPreferences(Context context) {
        // This sample app persists the registration ID in shared preferences, but
        // how you store the regID in your app is up to you.
        return context.getSharedPreferences(MainActivity.class.getSimpleName(),
                Context.MODE_PRIVATE);
    }
    /**
     * Set the registration ID for application on GCM service.
     */
    public static void setDeviceId(Context context, String newRegistrationId ) {
        final SharedPreferences prefs = getGCMPreferences(context);
        prefs.edit().putString(CGM_REG_ID, newRegistrationId).commit();
    }

    /**
     * Gets the current registration ID for application on GCM service.
     * <p>
     * If result is empty, the app needs to register.
     *
     * @return registration ID, or empty string if there is no existing
     *         registration ID.
     */
    public static String getDeviceId(Context context) {
        final SharedPreferences prefs = getGCMPreferences(context);
        String registrationId = prefs.getString(CGM_REG_ID, "");
        if (registrationId.isEmpty()) {
            Log.i(TAG, "Registration not found.");
            return "";
        }
//        // Check if app was updated; if so, it must clear the registration ID
//        // since the existing regID is not guaranteed to work with the new
//        // app version.
//        int registeredVersion = prefs.getInt(PROPERTY_APP_VERSION, Integer.MIN_VALUE);
//        int currentVersion = getAppVersion(context);
//        if (registeredVersion != currentVersion) {
//            Log.i(TAG, "App version changed.");
//            return "";
//        }
        return registrationId;
    }

    /**
     * @return Application's version code from the {@code PackageManager}.
     */
    private static int getAppVersion(Context context) {
        try {
            PackageInfo packageInfo = context.getPackageManager()
                    .getPackageInfo(context.getPackageName(), 0);
            return packageInfo.versionCode;
        } catch (PackageManager.NameNotFoundException e) {
            // should never happen
            throw new RuntimeException("Could not get package name: " + e);
        }
    }
}
