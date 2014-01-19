package com.blstream.ctfclient.network.requests;

import com.android.volley.AuthFailureError;
import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.toolbox.HttpHeaderParser;
import com.blstream.ctfclient.constants.CTFConstants;

import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Rafal on 17.01.14.
 */
public class TokenRequest extends Request<String> {
    public static final String URL_REQUEST = "oauth2/access_token/";
    private final Response.Listener<String> mListener;
    private Map<String, String> mParams;
    private String userName;
    private String password;

    /**
     * Creates a new request with the given method.
     *
     * @param method        the request {@link Method} to use
     * @param url           URL to fetch the string at
     * @param listener      Listener to receive the String response
     * @param errorListener Error listener, or null to ignore errors
     */
    public TokenRequest(int method, String url, String userName, String password, Response.Listener<String> listener,
                        Response.ErrorListener errorListener) {
        super(method, url, errorListener);
        this.mListener = listener;
        this.userName = userName;
        this.password = password;
    }

    @Override
    protected Response<String> parseNetworkResponse(NetworkResponse networkResponse) {
        String parsed;
        try {
            parsed = new String(networkResponse.data, HttpHeaderParser.parseCharset(networkResponse.headers));
        } catch (UnsupportedEncodingException e) {
            parsed = new String(networkResponse.data);
        }
        return Response.success(parsed, HttpHeaderParser.parseCacheHeaders(networkResponse));
    }

    @Override
    protected void deliverResponse(String response) {
        mListener.onResponse(response);
    }

    @Override
    protected Map<String, String> getParams() throws AuthFailureError {
        Map<String, String> params = new HashMap<>();
        params.put(CTFConstants.CLIENT_ID_PARAM_NAME, CTFConstants.CLIENT_ID);
        params.put(CTFConstants.CLIENT_SECRET_PARAM_NAME, CTFConstants.CLIENT_SECRET);
        params.put(CTFConstants.GRANT_TYPE_PARAM_NAME, CTFConstants.GrantType.PASSWORD.getValue());
        params.put(CTFConstants.SCOPE_PARAM_NAME, CTFConstants.Scope.READ_AND_WRITE.getValue());
        params.put(CTFConstants.USERNAME_PARAM_NAME, userName);
        params.put(CTFConstants.PASSWORD_PARAM_NAME, password);
        return params;
    }

    @Override
    public Map<String, String> getHeaders() throws AuthFailureError {
        Map<String, String> headers = super.getHeaders();
        return headers;
    }
}
