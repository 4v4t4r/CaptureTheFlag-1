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

    private LocationRequest mLocationRequest;
    private LocationClient mLocationClient;

    private final IBinder mBinder = new LocalBinder();

    private CTFLocationInterface mCtfLocationInterface;

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {

        mLocationClient = new LocationClient(this, this, this);

        mLocationRequest = LocationRequest.create();
        // Use high accuracy
        mLocationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        // Set the update interval to 5 seconds
        mLocationRequest.setInterval(UPDATE_INTERVAL);
        // Set the fastest update interval to 1 second
        mLocationRequest.setFastestInterval(FASTEST_INTERVAL);

       /* LocationManager service = (LocationManager) getSystemService(LOCATION_SERVICE);
        boolean enabled = service.isProviderEnabled(LocationManager.GPS_PROVIDER);

        // check if enabled and if not send user to the GSP settings
        // Better solution would be to display a dialog and suggesting to
        // go to the settings
        if (!enabled) {
            Intent intent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
            startActivity(intent);
        }
*/


        return super.onStartCommand(intent, flags, startId);
    }


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

        mLocationClient.connect();
        return mBinder;
    }

    @Override
    public void onConnected(Bundle bundle) {
        mLocationClient.requestLocationUpdates(mLocationRequest, this);
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