package com.blstream.ctfclient.network;

import android.content.Context;
import android.util.Log;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by Rafal on 17.01.14.
 */
public class ErrorHelper {

    public static final String KEY_INVALID_USERNAME = "username";
    public static final String KEY_INVALID_EMAIL = "email";
    public static final String KEY_ERROR = "error";
    public static final String KEY_DETAIL = "detail";

    /**
     * Returns appropriate message which is to be displayed to the user
     * against the specified error object.
     *
     * @param error
     * @param context
     * @return
     */
    public static String getMessage(Object error, Context context) {
//        if (error instanceof TimeoutError) {
//            return context.getResources().getString(R.string.generic_server_down);
//        } else if (isServerProblem(error)) {
//            return handleServerError(error, context);
//        } else if (isNetworkProblem(error)) {
//            return context.getResources().getString(R.string.no_internet);
//        }
        return context.getResources().getString(R.string.generic_error);
    }

    /**
     * Determines whether the error is related to network
     *
     * @param error
     * @return
     */
//    private static boolean isNetworkProblem(Object error) {
//        return (error instanceof NetworkError) || (error instanceof NoConnectionError);
//    }
//
//    /**
//     * Determines whether the error is related to server
//     *
//     * @param error
//     * @return
//     */
//    private static boolean isServerProblem(Object error) {
//        return (error instanceof ServerError) || (error instanceof AuthFailureError);
//    }

    /**
     * Handles the server error, tries to determine whether to show a stock message or to
     * show a message retrieved from the server.
     *
     * @param err
     * @param context
     * @return
     */
    private static String handleServerError(Object err, Context context) {
//
//        if (response != null) {
//            switch (response.statusCode) {
//                case 400:
//                case 404:
//                case 422:
//                case 401:
//                    try {
//                        String resultString = new String(response.data);
//                        JSONObject jsonObject = new JSONObject(resultString);
//
//                        String message = null;
//                        if (getMessageFromArray(jsonObject, KEY_INVALID_USERNAME) != null) {
//                            message = getMessageFromArray(jsonObject, KEY_INVALID_USERNAME);
//                        } else if (getMessageFromArray(jsonObject, KEY_INVALID_EMAIL) != null) {
//                            message = getMessageFromArray(jsonObject, KEY_INVALID_EMAIL);
//                        } else if (getMessage(jsonObject, KEY_ERROR) != null) {
//                            message = getMessage(jsonObject, KEY_ERROR);
//                        } else if (getMessage(jsonObject, KEY_DETAIL) != null){
//                            message = getMessage(jsonObject, KEY_DETAIL);
//                        }
//                        return message;
//                    } catch (Exception e) {
//                        Log.e(CTF.TAG, "Exception", e);
//                    }
//                    // invalid request
//                    return error.getMessage();
//
//                default:
//                    return context.getResources().getString(R.string.generic_server_down);
//            }
//        }
        return context.getResources().getString(R.string.generic_error);
    }

    private static String getMessage(JSONObject jsonObject, String key) {
        String message = null;
        try {
            message = (String) jsonObject.get(key);
        } catch (JSONException e) {
            Log.d(CTF.TAG, "JSONException", e);
        }
        return message;
    }

    private static String getMessageFromArray(JSONObject jsonObject, String key) {
        String message = null;
        try {
            JSONArray jsonArray = (JSONArray) jsonObject.get(key);
            if (jsonArray != null) {
                message = (String) jsonArray.get(0);
            }
        } catch (JSONException e) {
            Log.d(CTF.TAG, "JSONException", e);
        }
        return message;
    }

}
