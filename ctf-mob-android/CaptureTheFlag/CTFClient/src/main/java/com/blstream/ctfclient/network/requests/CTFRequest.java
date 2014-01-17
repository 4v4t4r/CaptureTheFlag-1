package com.blstream.ctfclient.network.requests;

import com.android.volley.Response;
import com.android.volley.toolbox.JsonObjectRequest;
import org.json.JSONObject;

/**
 * Created by Rafal on 17.01.14.
 */
public class CTFRequest extends JsonObjectRequest {

	public static final String PARAM_USERS = "api/users/";

	public CTFRequest(int method, String url, JSONObject jsonRequest, Response.Listener<JSONObject> listener, Response.ErrorListener errorListener) {
		super(method, url, jsonRequest, listener, errorListener);
	}
}
