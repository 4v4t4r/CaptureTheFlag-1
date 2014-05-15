package com.blstream.ctfclient.activities;

import android.app.AlertDialog;
import android.app.Fragment;
import android.app.FragmentManager;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.json.RegisterPlayerPositionResponse;
import com.blstream.ctfclient.network.requests.CTFGetGameRequest;
import com.blstream.ctfclient.utils.GameBorderTask;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.Circle;
import com.google.android.gms.maps.model.CircleOptions;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.PolygonOptions;
import com.octo.android.robospice.persistence.DurationInMillis;
import com.octo.android.robospice.persistence.exception.SpiceException;
import com.octo.android.robospice.request.listener.RequestListener;

/**
 * Created by Rafał Zadrożny on 1/27/14.
 */
public class MapActivity extends CTFBaseActivity implements GameBorderTask.OnGameBorderTaskListener {

    private static final String TAG = MapActivity.class.getSimpleName();

    private static final float DEGREES_OF_TILT = 30.0f;
    private static final int ANIMATION_DURATION_MS = 1000;
    private static float CURRENT_ZOOM = 17;
    private final int RQS_GooglePlayServices = 1;
    private GoogleMap googleMap;

    private long mGameId;
    private Game mGame;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);

        Bundle data = getIntent().getExtras();
        mGameId = data.getLong(GamesActivity.EXTRA_GAME_ID);

        checkGooglePlayServices();

        if (!initializeMap()) {
            Toast.makeText(getApplicationContext(),
                    "Sorry! unable to create maps", Toast.LENGTH_SHORT).show();
            finish();
        }
    }

    private class RegisterPlayerPositionRequestListener implements RequestListener<RegisterPlayerPositionResponse> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.d(TAG, "onRequestFailure " + spiceException.getLocalizedMessage());
        }

        @Override
        public void onRequestSuccess(RegisterPlayerPositionResponse response) {
            Log.d(TAG, "onRequestSuccess " + response.toString() + " " + response.getMarkers().size());

        }
    }

    private class GetGameRequestListener implements RequestListener<Game> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.d(TAG, "onRequestFailure " + spiceException.getLocalizedMessage());
        }

        @Override
        public void onRequestSuccess(Game response) {
            Log.d(TAG, "onRequestSuccess " + response.toString() + " " + response.toString());

            mGame = response;

            moveCamera(mGame.getLocation().toLatLng());

            addFlagToMap(new LatLng(53.4275, 14.5518), "Our flag", "Defend your flag against the enemy.", R.drawable.flag_blue);
            addFlagToMap(new LatLng(53.4295, 14.5538), "Their flag", "The flag of the enemy", R.drawable.flag_red);
            addCharacter(new LatLng(53.4202, 14.5549), 200, "Rafał", "From blue team.");

            GameBorderTask gameBorderTask = new GameBorderTask(mGame.getLocation().toLatLng(), mGame.getRadius(), MapActivity.this);
            gameBorderTask.execute();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        super.onCreateOptionsMenu(menu);
        getMenuInflater().inflate(R.menu.map, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.menu_legalnotices:
                String licenseInfo = GooglePlayServicesUtil.getOpenSourceSoftwareLicenseInfo(
                        getApplicationContext());
                AlertDialog.Builder LicenseDialog = new AlertDialog.Builder(MapActivity.this);
                LicenseDialog.setTitle("Legal Notices");
                LicenseDialog.setMessage(licenseInfo);
                LicenseDialog.show();
                return true;
        }
        return super.onOptionsItemSelected(item);
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
            Log.e("Error: GooglePlayServiceUtil: ", "", e);
        }
    }

    /**
     * function to load map. If map is not created it will create it for you
     */
    private boolean initializeMap() {
        try {
            if (googleMap == null) {
                FragmentManager fragmentManager = getFragmentManager();
                Fragment fragment = fragmentManager.findFragmentById(R.id.map);
                googleMap = ((MapFragment) fragment).getMap();
                setDirectionButton();

                // check if map is created successfully or not
                if (googleMap == null) {
                    return false;
                }
            }
            googleMap.setMyLocationEnabled(true);
            googleMap.setBuildingsEnabled(true);
            googleMap.getUiSettings().setCompassEnabled(false);
            googleMap.getUiSettings().setZoomControlsEnabled(false);
            googleMap.getUiSettings().setZoomGesturesEnabled(true);
            googleMap.getUiSettings().setRotateGesturesEnabled(true);
            googleMap.getUiSettings().setMyLocationButtonEnabled(false);
            googleMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);

            CTFGetGameRequest ctfGetGameRequest = new CTFGetGameRequest(mGameId);
            getSpiceManager().execute(ctfGetGameRequest, ctfGetGameRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new GetGameRequestListener());

        } catch (Exception e) {
            Log.d(CTF.TAG, "Exception", e);
            return false;
        }
        return true;
    }

    private void setDirectionButton() {
        Button directionBtn = (Button) findViewById(R.id.direction);
        directionBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                StringBuilder builder = new StringBuilder("http://maps.googleapis.com/maps/api/directions/json?origin=");
                builder.append("53.4202").append(",").append("14.5549");
                builder.append("&destination=");
                builder.append("53.4295").append(",").append("14.5538");
                builder.append("&sensor=false&mode=walking");
            }
        });
    }

    private void addCharacter(LatLng position, final int rangeOfVisibility, String name, String description) {
        MarkerOptions characterMarkerOptions = new MarkerOptions()
                .title(name)
                .snippet(description)
                .position(position)
                .anchor(0.5f, 0.5f)
                .flat(false)
                .icon(BitmapDescriptorFactory.fromResource(R.drawable.character));
        googleMap.addMarker(characterMarkerOptions);
        final Circle circle = googleMap.addCircle(new CircleOptions()
                .center(position)
                .strokeWidth(4f)
                .strokeColor(Color.BLUE)
                .radius(rangeOfVisibility));

        /*ValueAnimator vAnimator = new ValueAnimator();
        vAnimator.setRepeatCount(ValueAnimator.INFINITE);
        vAnimator.setRepeatMode(ValueAnimator.RESTART);
        vAnimator.setIntValues(0, rangeOfVisibility);
        vAnimator.setDuration(ANIMATION_DURATION_MS);
        vAnimator.setEvaluator(new IntEvaluator());
        vAnimator.setInterpolator(new AccelerateDecelerateInterpolator());
        vAnimator.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
            @Override
            public void onAnimationUpdate(ValueAnimator valueAnimator) {
                float animatedFraction = valueAnimator.getAnimatedFraction();
                circle.setRadius(animatedFraction * rangeOfVisibility);
            }
        });
        vAnimator.start();*/
    }

    private void moveCamera(LatLng latLng) {
        CameraPosition cameraPosition = new CameraPosition.Builder()
                .target(latLng)
                .zoom(CURRENT_ZOOM)
                .tilt(DEGREES_OF_TILT)
                .bearing(googleMap.getCameraPosition().bearing)
                .build();

        googleMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition), ANIMATION_DURATION_MS, null);
    }

    private void addFlagToMap(LatLng position, String name, String description, int iconID) {
        MarkerOptions myMarkerOptions = new MarkerOptions()
                .title(name)
                .snippet(description)
                .position(position)
                .anchor(0.0f, 1.0f)
                .icon(BitmapDescriptorFactory.fromResource(iconID));
        googleMap.addMarker(myMarkerOptions);
    }

    @Override
    public void onGameBorderTaskEnd(PolygonOptions border) {
        googleMap.addPolygon(border);
    }
}