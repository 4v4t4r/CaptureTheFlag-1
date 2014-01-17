package com.blstream.ctfclient.network.requests;

import android.util.Log;
import com.android.volley.AuthFailureError;
import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.HttpHeaderParser;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Rafal on 17.01.14.
 */
public class RegisterRequest extends Request<String> {

	/** Charset for request. */
	private static final String PROTOCOL_CHARSET = "utf-8";

	/** Content type for request. */
	private static final String PROTOCOL_CONTENT_TYPE = String.format("application/json; charset=%s", PROTOCOL_CHARSET);

	public static final String URL_REQUEST = "api/registration/";
	private final Response.Listener<String> mListener;
	private String mRequestBody;

	/**
	 * Creates a new request with the given method.
	 *
	 * @param method the request {@link Method} to use
	 * @param url URL to fetch the string at
	 * @param jsonString json
	 * @param listener Listener to receive the String response
	 * @param errorListener Error listener, or null to ignore errors
	 */
	public RegisterRequest(int method, String url, String jsonString, Response.Listener<String> listener,
	                    Response.ErrorListener errorListener) {
		super(method, url, errorListener);
		this.mListener = listener;
		this.mRequestBody = jsonString;
	}

	@Override
	protected Response<String> parseNetworkResponse(NetworkResponse networkResponse) {
		String parsed;
		try {
			parsed = new String(networkResponse.data, HttpHeaderParser.parseCharset(networkResponse.headers));
		} catch (UnsupportedEncodingException e) {
			parsed = new String(networkResponse.data);
		}
		Log.d("RegisterActivity", "parsed: " + parsed);
		return Response.success(parsed, HttpHeaderParser.parseCacheHeaders(networkResponse));
	}

	@Override
	protected void deliverResponse(String response) {
		mListener.onResponse(response);
	}

	/**
	 * @deprecated Use {@link #getBodyContentType()}.
	 */
	@Override
	public String getPostBodyContentType() {
		return getBodyContentType();
	}

	/**
	 * @deprecated Use {@link #getBody()}.
	 */
	@Override
	public byte[] getPostBody() {
		return getBody();
	}

	@Override
	public String getBodyContentType() {
		return PROTOCOL_CONTENT_TYPE;
	}

	@Override
	public byte[] getBody() {
		try {
			return mRequestBody == null ? null : mRequestBody.getBytes(PROTOCOL_CHARSET);
		} catch (UnsupportedEncodingException uee) {
			VolleyLog.wtf("Unsupported Encoding while trying to get the bytes of %s using %s",
					mRequestBody, PROTOCOL_CHARSET);
			return null;
		}
	}

	@Override
	public Map<String, String> getHeaders() throws AuthFailureError {
		Map<String, String> headers = new HashMap<>();
		headers.put("Accept", "application/json");
		headers.put("Content-type", PROTOCOL_CONTENT_TYPE);
		return headers;
	}
}