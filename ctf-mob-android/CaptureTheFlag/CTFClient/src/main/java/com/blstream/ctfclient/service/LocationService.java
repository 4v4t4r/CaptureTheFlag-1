package com.blstream.ctfclient.service;

import android.app.Service;
import android.content.Intent;
import android.location.Location;
import android.os.Binder;
import android.os.Bundle;
import android.os.IBinder;
import android.text.format.DateUtils;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesClient;
import com.google.android.gms.location.LocationClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;

public class LocationService extends Service implements GooglePlayServicesClient.ConnectionCallbacks, GooglePlayServicesClient.OnConnectionFailedListener, LocationListener {

    private static final int UPDATE_INTERVAL_IN_SECONDS = 5;
    private static final int FASTEST_INTERVAL_IN_SECONDS = 1;

    private static final long UPDATE_INTERVAL = DateUtils.SECOND_IN_MILLIS * UPDATE_INTERVAL_IN_SECONDS;
    private static final long FASTEST_INTERVAL = DateUtils.SECOND_IN_MILLIS * FASTEST_INTERVAL_IN_SECONDS;

    private LocationClient mLocationClient;

    private final IBinder mBinder = new LocalBinder();

    private CTFLocationInterface mCtfLocationInterface;

    public interface CTFLocationInterface {
        public void onLocationUpdate(Location location);

        public void onServiceDisconnected();

        public void onLocationServiceError();

    }

    public class LocalBinder extends Binder {
        public LocationService getService(CTFLocationInterface ctfLocationInterface) {
            mCtfLocationInterface = ctfLocationInterface;
            return LocationService.this;
        }
    }

    @Override
    public boolean onUnbind(Intent intent) {
        mCtfLocationInterface = null;
        mLocationClient.disconnect();
        return super.onUnbind(intent);
    }

    @Override
    public IBinder onBind(Intent intent) {
        mLocationClient = new LocationClient(this, this, this);
        mLocationClient.connect();
        return mBinder;
    }

    @Override
    public void onConnected(Bundle bundle) {

        LocationRequest locationRequest = LocationRequest.create();
        locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        locationRequest.setInterval(UPDATE_INTERVAL);
        locationRequest.setFastestInterval(FASTEST_INTERVAL);

        mLocationClient.requestLocationUpdates(locationRequest, this);
    }

    @Override
    public void onDisconnected() {
        mCtfLocationInterface.onServiceDisconnected();
    }

    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        mCtfLocationInterface.onLocationServiceError();
    }

    @Override
    public void onLocationChanged(Location location) {
        mCtfLocationInterface.onLocationUpdate(location);
    }
}