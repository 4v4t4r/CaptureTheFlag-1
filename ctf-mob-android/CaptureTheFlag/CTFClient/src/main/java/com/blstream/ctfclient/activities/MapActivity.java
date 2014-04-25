package com.blstream.ctfclient.activities;

import android.animation.IntEvaluator;
import android.animation.ValueAnimator;
import android.app.AlertDialog;
import android.app.Fragment;
import android.app.FragmentManager;
import android.graphics.Color;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.animation.AccelerateDecelerateInterpolator;
import android.widget.Button;
import android.widget.Toast;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
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
import com.google.android.gms.maps.model.Polygon;
import com.google.android.gms.maps.model.PolygonOptions;
import com.google.android.gms.maps.model.Polyline;
import com.google.android.gms.maps.model.PolylineOptions;
import com.google.maps.android.SphericalUtil;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Rafał Zadrożny on 1/27/14.
 */
public class MapActivity extends CTFBaseActivity {

    private static final float DEGREES_OF_TILT = 30.0f;
    private static final int ANIMATION_DURATION_MS = 1000;
    private final int RQS_GooglePlayServices = 1;
    public static float currentZoom = 17;
    private GoogleMap googleMap;
    private LatLng centerPoint;
    private Polygon polygon;
    private List<LatLng> hole = null;
    private List<LatLng> border = null;
    private boolean mapCreated = false;
    private int gameMapRadius;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);
        mapCreated = initilizeMap();
    }

    @Override
    protected void onResume() {
        super.onResume();
        checkGooglePlayServices();
        if(!mapCreated){
            if(!initilizeMap()){
                Toast.makeText(getApplicationContext(),
                        "Sorry! unable to create maps", Toast.LENGTH_SHORT).show();
                finish();
            }
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
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
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
     * */
    private boolean initilizeMap() {
        try{
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

            setCenterPointOfMap(new LatLng(53.4285, 14.5528));
            moveCamera(centerPoint);
            addFlagToMap(new LatLng(53.4275, 14.5518), "Our flag", "Defend your flag against the enemy.", R.drawable.flag_blue);
            addFlagToMap(new LatLng(53.4295, 14.5538), "Their flag", "The flag of the enemy", R.drawable.flag_red);
            addCharacter(new LatLng(53.4202, 14.5549), 200, "Rafał", "From blue team.");
            gameMapRadius = 1000;
            setGameMapBorders(gameMapRadius);
            googleMap.setOnCameraChangeListener(new GameCameraChangeListener());
        }catch (Exception e){
            Log.d(CTF.TAG, "Exception", e);
            return false;
        }
        return true;
    }

    private void setDirectionButton() {
        Button directionBtn = (Button) findViewById(R.id.direction);
        directionBtn.setOnClickListener( new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                StringBuilder builder = new StringBuilder("http://maps.googleapis.com/maps/api/directions/json?origin=");
                builder.append("53.4202").append(",").append("14.5549");
                builder.append("&destination=");
                builder.append("53.4295").append(",").append("14.5538");
                builder.append("&sensor=false&mode=walking");
//                StringRequest stringRequest = new StringRequest(Request.Method.GET, builder.toString(), new Response.Listener<String>() {
//                    @Override
//                    public void onResponse(String returnValue) {
//                        drawPolyline(returnValue);
//                    }
//                }, new Response.ErrorListener() {
//                    @Override
//                    public void onErrorResponse(VolleyError volleyError) {
//                        Log.e(CTF.TAG, "ERROR", volleyError.getCause());
//                    }
//                });
//                CTF.getInstance().addToRequestQueue(stringRequest);
            }
        });
    }

    private void drawPolyline(String returnValue) {
        try {
            JSONObject result = new JSONObject(returnValue);
            JSONArray routes = null;

            routes = result.getJSONArray("routes");

            long distanceForSegment = routes.getJSONObject(0).getJSONArray("legs").getJSONObject(0).getJSONObject("distance").getInt("value");

            JSONArray steps = routes.getJSONObject(0).getJSONArray("legs")
                    .getJSONObject(0).getJSONArray("steps");

            List<LatLng> lines = new ArrayList<LatLng>();

            for(int i=0; i < steps.length(); i++) {
                String polyline = steps.getJSONObject(i).getJSONObject("polyline").getString("points");

                for(LatLng p : decodePolyline(polyline)) {
                    lines.add(p);
                }
            }
            Polyline polylineToAdd = googleMap.addPolyline(new PolylineOptions().addAll(lines).width(3).color(Color.RED));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private List<LatLng> decodePolyline(String encoded) {
        List<LatLng> poly = new ArrayList<LatLng>();

        int index = 0, len = encoded.length();
        int lat = 0, lng = 0;

        while (index < len) {
            int b, shift = 0, result = 0;
            do {
                b = encoded.charAt(index++) - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } while (b >= 0x20);
            int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lat += dlat;

            shift = 0;
            result = 0;
            do {
                b = encoded.charAt(index++) - 63;
                result |= (b & 0x1f) << shift;
                shift += 5;
            } while (b >= 0x20);
            int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
            lng += dlng;

            LatLng p = new LatLng((double) lat / 1E5, (double) lng / 1E5);
            poly.add(p);
        }
        return poly;
    }

    private void setCenterPointOfMap(LatLng latLng) {
        this.centerPoint = latLng;
    }

    private void setGameMapBorders(final int distance) {

        new AsyncTask<Void, Void, Void>(){

            @Override
            protected Void doInBackground(Void... voids) {
                if( hole == null || border == null){
                    hole = new ArrayList<>();
                    border = new ArrayList<>();
                    int degree = 0;
                    for(int i = 0; i < 360; i++){
                        degree = i;
                        if(degree == 360){
                            hole.add(hole.get(0));
                        }else{
                            hole.add(SphericalUtil.computeOffset(centerPoint, distance, degree));
                        }
                    }
                    degree = 0;
                    for(int i = 0; i < 360; i++){
                        degree = i;
                        if(degree == 360){
                            border.add(border.get(0));
                        }else{
                            border.add(SphericalUtil.computeOffset(centerPoint, distance * 10, degree));
                        }
                    }
                }
                return (null);
            }

            @Override
            protected void onPostExecute(Void aVoid) {
                if(polygon != null){
                    polygon.remove();
                    polygon = null;
                }
                polygon = googleMap.addPolygon(new PolygonOptions()
                        .addAll(border)
                        .addHole(hole)
                        .fillColor(R.color.transparent_black)
                        .strokeColor(R.color.transparent_black)
                        .strokeWidth(8.0f)
                        .visible(true));
            }
        }.execute();

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

        ValueAnimator vAnimator = new ValueAnimator();
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
        vAnimator.start();
    }

    private void moveCamera(LatLng latLng) {
        CameraPosition cameraPosition = new CameraPosition.Builder()
                .target(latLng)
                .zoom(currentZoom)
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

    class GameCameraChangeListener implements GoogleMap.OnCameraChangeListener{
        @Override
        public void onCameraChange(CameraPosition cameraPosition) {

            if(cameraPosition.zoom != currentZoom){
                setGameMapBorders(gameMapRadius);
            }
            currentZoom = cameraPosition.zoom;
        }
    }

}