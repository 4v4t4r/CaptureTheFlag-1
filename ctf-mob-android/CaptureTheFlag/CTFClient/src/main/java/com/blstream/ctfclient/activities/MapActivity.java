package com.blstream.ctfclient.activities;

import android.app.AlertDialog;
import android.app.Fragment;
import android.app.FragmentManager;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.Marker;
import com.blstream.ctfclient.model.dto.json.RegisterPlayerPositionResponse;
import com.blstream.ctfclient.network.requests.CTFGetGameRequest;
import com.blstream.ctfclient.network.requests.CTFRegisterPlayerPositionRequest;
import com.blstream.ctfclient.utils.GameBorderTask;
import com.blstream.ctfclient.utils.UserLocationSimulation;
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
    private UserLocationSimulation mUserLocationSimulation;

    private static final float DEGREES_OF_TILT = 30.0f;
    private static final int ANIMATION_DURATION_MS = 1000;
    private final int RQS_GooglePlayServices = 1;

    private GoogleMap mGoogleMap;
    private float mVisibilityRange;
    private long mGameId;
    private Handler mHandler;

    private Circle mCharacterCircle;
    private com.google.android.gms.maps.model.Marker mCharacter;

    private TextView mGameInfo;

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


        mGameInfo = (TextView) findViewById(R.id.gameInfo);
        mHandler = new Handler(Looper.getMainLooper());
    }

    private class RegisterPlayerPositionRequestListener implements RequestListener<RegisterPlayerPositionResponse> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.d(TAG, "onRequestFailure " + spiceException.getLocalizedMessage());
        }

        @Override
        public void onRequestSuccess(RegisterPlayerPositionResponse response) {
            Log.d(TAG, "onRequestSuccess " + response.toString() + " " + response.getMarkers().size());

            mGameInfo.setText(response.getGameSummary().toString());

            for (Marker marker : response.getMarkers()) {
                switch (marker.getType()) {
                    case BLUE_BASE: {
                        addMarkerToMap(marker.getLocation().toLatLng(), "Blue base", "Blue base", R.drawable.blue_base);
                        break;
                    }
                    case RED_BASE: {
                        addMarkerToMap(marker.getLocation().toLatLng(), "Red base", "Red base", R.drawable.red_base);
                        break;
                    }
                    case BLUE_FLAG: {
                        addMarkerToMap(marker.getLocation().toLatLng(), "Blue flag", "Blue flag", R.drawable.flag_blue);
                        break;
                    }
                    case RED_FLAG: {
                        addMarkerToMap(marker.getLocation().toLatLng(), "Red flag", "Red flag", R.drawable.flag_red);
                        break;
                    }
                    case PLAYER: {
                        addCharacter(marker.getLocation().toLatLng(), mVisibilityRange, "Me", "Me", R.drawable.character);
                        break;
                    }
                }

                mHandler.removeCallbacksAndMessages(null);
                mHandler.postDelayed(updateGameRunnable, 1000);
            }
        }
    }

    private final Runnable updateGameRunnable = new Runnable() {
        @Override
        public void run() {
            updateGameData();
            mHandler.postDelayed(this, 1000);
        }
    };

    private class GetGameRequestListener implements RequestListener<Game> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.d(TAG, "onRequestFailure " + spiceException.getLocalizedMessage());
        }

        @Override
        public void onRequestSuccess(Game response) {
            Log.d(TAG, "onRequestSuccess " + response.toString() + " " + response.toString());

            moveCamera(response.getLocation().toLatLng(), response.getRadius());

            GameBorderTask gameBorderTask = new GameBorderTask(response.getLocation().toLatLng(), response.getRadius(), MapActivity.this);
            gameBorderTask.execute();

            mUserLocationSimulation = new UserLocationSimulation(response.getLocation());

            mVisibilityRange = response.getVisibilityRange();

            updateGameData();
        }
    }

    private void updateGameData() {
        mUserLocationSimulation.moveRandom();

        addCharacter(mUserLocationSimulation.getLocation().toLatLng(), mVisibilityRange, "Me", "Me", R.drawable.bubble_orange);
        CTFRegisterPlayerPositionRequest registerPlayerPositionRequest = new CTFRegisterPlayerPositionRequest(mGameId, mUserLocationSimulation.getLocation());
        getSpiceManager().execute(registerPlayerPositionRequest, registerPlayerPositionRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new RegisterPlayerPositionRequestListener());
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
                String licenseInfo = GooglePlayServicesUtil.getOpenSourceSoftwareLicenseInfo(getApplicationContext());
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
            if (mGoogleMap == null) {
                FragmentManager fragmentManager = getFragmentManager();
                Fragment fragment = fragmentManager.findFragmentById(R.id.map);
                mGoogleMap = ((MapFragment) fragment).getMap();
                setDirectionButton();

                // check if map is created successfully or not
                if (mGoogleMap == null) {
                    return false;
                }
            }
            mGoogleMap.setMyLocationEnabled(true);
            mGoogleMap.setBuildingsEnabled(true);
            mGoogleMap.getUiSettings().setCompassEnabled(false);
            mGoogleMap.getUiSettings().setZoomControlsEnabled(false);
            mGoogleMap.getUiSettings().setZoomGesturesEnabled(true);
            mGoogleMap.getUiSettings().setRotateGesturesEnabled(true);
            mGoogleMap.getUiSettings().setMyLocationButtonEnabled(false);
            mGoogleMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);

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

    private void addCharacter(LatLng position, final float rangeOfVisibility, String name, String description, int id) {

        if (mCharacter != null) {
            mCharacter.remove();
        }

        if (mCharacterCircle != null) {
            mCharacterCircle.remove();
        }

        MarkerOptions characterMarkerOptions = new MarkerOptions()
                .title(name)
                .snippet(description)
                .position(position)
                .anchor(0.5f, 0.5f)
                .flat(false)
                .icon(BitmapDescriptorFactory.fromResource(id));

        mCharacter = mGoogleMap.addMarker(characterMarkerOptions);
        mCharacterCircle = mGoogleMap.addCircle(new CircleOptions()
                .center(position)
                .strokeWidth(4f)
                .strokeColor(Color.BLUE)
                .visible(true)
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

    private void moveCamera(LatLng latLng, int radius) {
        CameraPosition cameraPosition = new CameraPosition.Builder()
                .target(latLng)
                .zoom(getZoomLevel(radius))
                .tilt(DEGREES_OF_TILT)
                .bearing(mGoogleMap.getCameraPosition().bearing)
                .build();

        mGoogleMap.animateCamera(CameraUpdateFactory.newCameraPosition(cameraPosition), ANIMATION_DURATION_MS, null);
    }

    private void addMarkerToMap(LatLng position, String name, String description, int iconID) {
        MarkerOptions myMarkerOptions = new MarkerOptions()
                .title(name)
                .snippet(description)
                .position(position)
                .anchor(0.5f, 0.5f)
                .icon(BitmapDescriptorFactory.fromResource(iconID));
        mGoogleMap.addMarker(myMarkerOptions);
    }

    public float getZoomLevel(int radius) {
        double scale = radius / 280.0;
        float zoomLevel = (float) (17 - Math.log(scale) / Math.log(2));
        return zoomLevel;
    }

    @Override
    public void onGameBorderTaskEnd(PolygonOptions border) {
        mGoogleMap.addPolygon(border);
    }
}