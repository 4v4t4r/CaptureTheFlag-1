package com.blstream.ctfclient.activities;

import android.content.Context;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.util.Log;
import android.view.KeyEvent;
import android.webkit.ConsoleMessage;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;

/**
 * Created by Rafał Zadrożny on 11.02.14.
 */
public class MapWebActivity extends CTFBaseActivity implements LocationListener {

    private WebView webView;
    private Location mostRecentLocation;

    @Override
    /** Called when the activity is first created. */
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_web_map);
        getLocation();
        setupWebView();
    }

    private void setupWebView(){
        final String centerURL = ( mostRecentLocation != null ) ? "javascript:centerAt(" + mostRecentLocation.getLatitude() + "," + mostRecentLocation.getLongitude()+ ")" : null;
        webView = (WebView) findViewById(R.id.webview);
        webView.getSettings().setJavaScriptEnabled(true);
        //Wait for the page to load then send the location information
        webView.setWebViewClient(new WebViewClient(){
            @Override
            public void onPageFinished(WebView view, String url){
                if( centerURL != null ){
                    webView.loadUrl(centerURL);
                }
            }

        });
        webView.setWebChromeClient(new WebChromeClient() {


            @Override
            public boolean onConsoleMessage(ConsoleMessage cm) {
                Log.d(CTF.TAG, cm.message() + " -- From line "
                        + cm.lineNumber() + " of "
                        + cm.sourceId() );
                return true;
            }
        });
        webView.loadUrl("file:///android_asset/www/gamemap.html");
    }


    /**
     * The Location Manager manages location providers. This code searches
     * for the best provider of data (GPS, WiFi/cell phone tower lookup,
     * some other mechanism) and finds the last known location.
     **/
    private void getLocation() {
        LocationManager locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
        Criteria criteria = new Criteria();
        criteria.setAccuracy(Criteria.ACCURACY_FINE);
        String provider = locationManager.getBestProvider(criteria,true);

        //In order to make sure the device is getting location, request updates.        locationManager.requestLocationUpdates(provider, 1, 0, this);
        mostRecentLocation = locationManager.getLastKnownLocation(provider);
    }

    @Override
    public void onLocationChanged(Location location) {
        mostRecentLocation = location;
    }

    @Override
    public void onProviderDisabled(String provider) {
    }

    @Override
    public void onProviderEnabled(String provider) {
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras) {
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        // Check if the key event was the Back button and if there's history
        if ((keyCode == KeyEvent.KEYCODE_BACK) && webView.canGoBack()) {
            webView.goBack();
            return true;
        }
        // If it wasn't the Back key or there's no web page history, bubble up to the default
        // system behavior (probably exit the activity)
        return super.onKeyDown(keyCode, event);
    }
}
