package com.blstream.ctfclient.activities;

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.maps.MapFragment;

/**
 * Created by Rafał Zadrożny on 1/27/14.
 */
public class MapActivity extends Activity {

    final int RQS_GooglePlayServices = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);

        if (savedInstanceState == null) {
            getFragmentManager().beginTransaction().add(R.id.container, new MapFragment()).commit();
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    @Override
    protected void onResume() {
        super.onResume();
        checkGooglePlayServices();

    }

    private void checkGooglePlayServices() {
        // Check status of Google Play Services
        int status = GooglePlayServicesUtil.isGooglePlayServicesAvailable(CTF.getStaticApplicationContext());

        // Check Google Play Service Available
        try {
            if (status != ConnectionResult.SUCCESS) {
                GooglePlayServicesUtil.getErrorDialog(status, this, RQS_GooglePlayServices).show();
            }
        } catch (Exception e) {
            Log.e("Error: GooglePlayServiceUtil: ", "" + e);
        }
    }
}
