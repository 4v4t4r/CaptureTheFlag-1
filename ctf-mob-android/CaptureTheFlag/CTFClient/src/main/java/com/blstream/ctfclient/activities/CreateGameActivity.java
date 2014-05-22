package com.blstream.ctfclient.activities;

import android.app.Fragment;
import android.app.FragmentManager;
import android.graphics.Color;
import android.location.Location;
import android.os.Bundle;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.Item;
import com.blstream.ctfclient.network.requests.CTFCreateGameRequest;
import com.blstream.ctfclient.network.requests.CTFCreateItemRequest;
import com.fourmob.datetimepicker.date.DatePickerDialog;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.Circle;
import com.google.android.gms.maps.model.CircleOptions;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.octo.android.robospice.persistence.DurationInMillis;
import com.octo.android.robospice.persistence.exception.SpiceException;
import com.octo.android.robospice.request.listener.RequestListener;
import com.sleepbot.datetimepicker.time.RadialPickerLayout;
import com.sleepbot.datetimepicker.time.TimePickerDialog;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.GregorianCalendar;
import java.util.HashMap;
import java.util.Locale;
import java.util.Stack;
import java.util.TimeZone;

import butterknife.ButterKnife;
import butterknife.InjectView;
import butterknife.OnClick;

public class CreateGameActivity extends CTFBaseActivity implements DatePickerDialog.OnDateSetListener, TimePickerDialog.OnTimeSetListener, GoogleMap.OnMapClickListener, GoogleMap.OnMarkerDragListener {
    public static final String DATEPICKER_TAG = "datepicker";
    public static final String TIMEPICKER_TAG = "timepicker";

    private static final String TAG = CreateGameActivity.class.getSimpleName();

    private GoogleMap googleMap;
    private CTFCreateGameRequest gameRequest;

    @InjectView(R.id.game_date_picker_button)
    Button btnSelectDate;
    @InjectView(R.id.game_time_picker_button)
    Button btnSelectTime;
    @InjectView(R.id.games_selected_start_date)
    TextView selectedStartDateTextView;

    @InjectView(R.id.games_name_edit_text)
    EditText gameNameEditText;
    @InjectView(R.id.games_description_edit_text)
    EditText gameDescriptionEditText;
    @InjectView(R.id.max_players_edit_text)
    EditText maxPlayerEditText;
    @InjectView(R.id.game_status_spinner)
    Spinner gameStatusSpinner;
    @InjectView(R.id.game_type_spinner)
    Spinner gameTypeSpiner;
    @InjectView(R.id.game_visibility_range_edit_text)
    EditText gameVisibilityRangeEditText;
    @InjectView(R.id.game_action_range_edit_text)
    EditText gameActionRangeEditText;

    static final int DATE_DIALOG_ID = 0;
    static final int TIME_DIALOG_ID = 1;

    Circle mRadiusCircle;
    Stack<TempMapItem> mItemsToAdd;

    HashMap<MapItemType, Marker> mMapItemTypeToMarker;
    HashMap<String, MapItemType> mMarkerIdToType;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_game);
        ButterKnife.inject(this);
        initView(savedInstanceState);

        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTimeZone(TimeZone.getDefault());
        SimpleDateFormat isoFormat = new SimpleDateFormat(CTF.DATE_FORMAT, Locale.getDefault());

        mMapItemTypeToMarker = new HashMap<>();
        mMarkerIdToType = new HashMap<>();

        fillItemsList();

        if (googleMap == null) {
            FragmentManager fragmentManager = getFragmentManager();
            Fragment fragment = fragmentManager.findFragmentById(R.id.map);
            googleMap = ((MapFragment) fragment).getMap();
            googleMap.setOnMapClickListener(this);
            googleMap.setOnMarkerDragListener(this);
        }
    }

    private void fillItemsList() {
        mItemsToAdd = new Stack<>();
        mItemsToAdd.push(new TempMapItem(MapItemType.RED_BASE, "red_base", "red_base", R.drawable.red_base));
        mItemsToAdd.push(new TempMapItem(MapItemType.BLUE_BASE, "blue_base", "blue_base", R.drawable.blue_base));
        mItemsToAdd.push(new TempMapItem(MapItemType.RADIUS_POINT, "Radius", "Radius", R.drawable.bubble_purple));
        mItemsToAdd.push(new TempMapItem(MapItemType.GAME_LOCATION, "Center point", "CenterPoint", R.drawable.bubble_blue));
    }

    private void drawRadiusCircle(Marker marker) {

        if (!mMarkerIdToType.get(marker.getId()).equals(MapItemType.RADIUS_POINT)) {
            return;
        }

        if (mRadiusCircle != null) {
            mRadiusCircle.remove();
        }

        Marker gameCenter = mMapItemTypeToMarker.get(MapItemType.GAME_LOCATION);
        float radius = calculateDistanceBetweenMarkers(gameCenter, marker);

        mRadiusCircle = googleMap.addCircle(new CircleOptions()
                .center(gameCenter.getPosition())
                .radius(radius)
                .strokeWidth(2)
                .strokeColor(Color.GRAY));
    }

    private float calculateDistanceBetweenMarkers(Marker from, Marker to) {
        LatLng center = from.getPosition();
        Location fromLocation = new Location("");
        fromLocation.setLongitude(center.longitude);
        fromLocation.setLatitude(center.latitude);

        LatLng radius = to.getPosition();
        Location toLocation = new Location("");
        toLocation.setLongitude(radius.longitude);
        toLocation.setLatitude(radius.latitude);
        return fromLocation.distanceTo(toLocation);
    }

    @Override
    public void onMapClick(LatLng latLng) {
        if (!mItemsToAdd.isEmpty()) {
            TempMapItem itemToAdd = mItemsToAdd.pop();

            Marker newMarker = addMarkerToMap(latLng, itemToAdd);

            mMapItemTypeToMarker.put(itemToAdd.getMapItemType(), newMarker);
            mMarkerIdToType.put(newMarker.getId(), itemToAdd.getMapItemType());

            drawRadiusCircle(newMarker);
        }
    }

    @Override
    public void onMarkerDragStart(Marker marker) {
        marker.hideInfoWindow();
        drawRadiusCircle(marker);
    }

    @Override
    public void onMarkerDrag(Marker marker) {
        drawRadiusCircle(marker);
    }

    @Override
    public void onMarkerDragEnd(Marker marker) {
    }

    enum MapItemType {
        GAME_LOCATION,
        RADIUS_POINT,
        BLUE_BASE,
        RED_BASE
    }

    private class TempMapItem {

        private int mIcon;
        private String mDescription;
        private String mName;
        private MapItemType mMapItemType;

        public TempMapItem(MapItemType mapItem, String name, String description, int icon) {
            this.mMapItemType = mapItem;
            this.mDescription = description;
            this.mName = name;
            this.mIcon = icon;
        }

        public int getIcon() {
            return mIcon;
        }

        public String getDescription() {
            return mDescription;
        }

        public String getName() {
            return mName;
        }

        public MapItemType getMapItemType() {
            return mMapItemType;
        }
    }

    private Marker addMarkerToMap(LatLng position, TempMapItem mapItem) {
        MarkerOptions myMarkerOptions = new MarkerOptions()
                .title(mapItem.getName())
                .snippet(mapItem.getDescription())
                .position(position)
                .anchor(0.5f, 0.5f)
                .draggable(true)
                .icon(BitmapDescriptorFactory.fromResource(mapItem.getIcon()));
        return googleMap.addMarker(myMarkerOptions);
    }

    private void initView(Bundle savedInstanceState) {

        // Game status spinner
        ArrayAdapter gameStatusAdapter = new ArrayAdapter(this, android.R.layout.simple_spinner_item, Game.GameStatus.values());
        gameStatusAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        gameStatusSpinner.setAdapter(gameStatusAdapter);

        // Game type spinner
        ArrayAdapter gameTypeAdapter = new ArrayAdapter(this, android.R.layout.simple_spinner_item, Game.GameType.values());
        gameTypeAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        gameTypeSpiner.setAdapter(gameTypeAdapter);
    }


    @OnClick(R.id.game_date_picker_button)
    public void onDateButonClick() {
        Calendar calendar = Calendar.getInstance();
        DatePickerDialog datePickerDialog = DatePickerDialog.newInstance(this, calendar.get(Calendar.YEAR), calendar.get(Calendar.MONTH), calendar.get(Calendar.DAY_OF_MONTH), false);
        datePickerDialog.setYearRange(calendar.get(Calendar.YEAR), 2037);
        datePickerDialog.show(getSupportFragmentManager(), DATEPICKER_TAG);
    }

    @OnClick(R.id.game_time_picker_button)
    public void onTimeButtonClick() {
        Calendar calendar = Calendar.getInstance();
        TimePickerDialog timePickerDialog = TimePickerDialog.newInstance(this, Calendar.getInstance().get(Calendar.HOUR_OF_DAY), calendar.get(Calendar.MINUTE), true, false);
        timePickerDialog.show(getSupportFragmentManager(), TIMEPICKER_TAG);
    }

    @OnClick(R.id.game_create_button)
    public void onCreateGameButtonClick() {

        if (!mItemsToAdd.isEmpty()) {
            Toast.makeText(getApplicationContext(), "Add all markers to map!", Toast.LENGTH_LONG).show();
            return;
        }

        Game game = new Game();
        game.setName(gameNameEditText.getText().toString());
        game.setDescription(gameDescriptionEditText.getText().toString());
        game.setStartTime(selectedStartDateTextView.getText().toString());
        game.setStatus(((Game.GameStatus) gameStatusSpinner.getSelectedItem()));
        game.setType((Game.GameType) gameTypeSpiner.getSelectedItem());

        game.setMaxPlayers(Integer.valueOf(maxPlayerEditText.getText().toString()));
        game.setVisibilityRange(Integer.valueOf(gameVisibilityRangeEditText.getText().toString()));
        game.setActionRange(Integer.valueOf(gameActionRangeEditText.getText().toString()));

        game.setRadius((int) calculateDistanceBetweenMarkers(mMapItemTypeToMarker.get(MapItemType.GAME_LOCATION), mMapItemTypeToMarker.get(MapItemType.RADIUS_POINT)));
        game.setLocation(new com.blstream.ctfclient.model.dto.Location(mMapItemTypeToMarker.get(MapItemType.GAME_LOCATION).getPosition()));

        createGame(game);
    }

    private void createGame(final Game game) {
        gameRequest = new CTFCreateGameRequest(game);
        getSpiceManager().execute(gameRequest, gameRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new GameRequestListener());
    }

    private void addRedBase(String url) {
        Item item = new Item();
        item.setLocation(new com.blstream.ctfclient.model.dto.Location(mMapItemTypeToMarker.get(MapItemType.RED_BASE).getPosition()));
        item.setType(Item.ItemType.RED_BASE);
        item.setGame(url);
        item.setName("RED_BASE");

        sendItem(item);
    }

    private void addRedFlag(String url) {
        Item item = new Item();
        item.setLocation(new com.blstream.ctfclient.model.dto.Location(mMapItemTypeToMarker.get(MapItemType.RED_BASE).getPosition()));
        item.setType(Item.ItemType.RED_FLAG);
        item.setGame(url);
        item.setName("RED_FLAG");

        sendItem(item);
    }

    private void addBlueBase(String url) {
        Item item = new Item();
        item.setLocation(new com.blstream.ctfclient.model.dto.Location(mMapItemTypeToMarker.get(MapItemType.BLUE_BASE).getPosition()));
        item.setType(Item.ItemType.BLUE_BASE);
        item.setGame(url);
        item.setName("BLUE_BASE");

        sendItem(item);
    }

    private void addBlueFlag(String url) {
        Item item = new Item();
        item.setLocation(new com.blstream.ctfclient.model.dto.Location(mMapItemTypeToMarker.get(MapItemType.BLUE_BASE).getPosition()));
        item.setType(Item.ItemType.BLUE_FLAG);
        item.setGame(url);
        item.setName("BLUE_FLAG");

        sendItem(item);
    }

    private void sendItem(Item item) {
        CTFCreateItemRequest ctfCreateItemRequest = new CTFCreateItemRequest(item);
        getSpiceManager().execute(ctfCreateItemRequest, ctfCreateItemRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new RequestListener<Item>() {
            @Override
            public void onRequestFailure(SpiceException spiceException) {
                Log.d(TAG, "onRequestFailure " + spiceException.getLocalizedMessage());
            }

            @Override
            public void onRequestSuccess(Item item) {
                Log.d(TAG, "onRequestSuccess " + item.getUrl());
            }
        });
    }

    private void updateSelectedStartDate(int dialogType, int year, int month, int day, int hour, int minute) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTimeZone(TimeZone.getDefault());
        String text = selectedStartDateTextView.getText().toString();
        SimpleDateFormat isoFormat = new SimpleDateFormat(CTF.DATE_FORMAT, Locale.getDefault());
        try {
            calendar.setTime(isoFormat.parse(text));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        switch (dialogType) {
            case DATE_DIALOG_ID:
                calendar.set(Calendar.YEAR, year);
                calendar.set(Calendar.MONTH, month);
                calendar.set(Calendar.DAY_OF_MONTH, day);
                calendar.set(Calendar.HOUR_OF_DAY, calendar.get(Calendar.HOUR_OF_DAY));
                calendar.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE));
                break;
            case TIME_DIALOG_ID:
                calendar.set(Calendar.YEAR, calendar.get(Calendar.YEAR));
                calendar.set(Calendar.MONTH, calendar.get(Calendar.MONTH));
                calendar.set(Calendar.DAY_OF_MONTH, calendar.get(Calendar.DAY_OF_MONTH));
                calendar.set(Calendar.HOUR_OF_DAY, hour);
                calendar.set(Calendar.MINUTE, minute);
                break;
        }

        selectedStartDateTextView.setText(isoFormat.format(calendar.getTime()));
    }

    @Override
    public void onDateSet(DatePickerDialog datePickerDialog, int year, int month, int day) {
        updateSelectedStartDate(DATE_DIALOG_ID, year, month, day, 0, 0);
    }

    @Override
    public void onTimeSet(RadialPickerLayout view, int hourOfDay, int minute) {
        updateSelectedStartDate(TIME_DIALOG_ID, 0, 0, 0, hourOfDay, minute);
    }

    private class GameRequestListener implements RequestListener<Game> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.d(TAG, "GameRequestListener.onRequestFailure " + spiceException.getLocalizedMessage());
        }

        @Override
        public void onRequestSuccess(Game game) {
            Log.d(TAG, "GameRequestListener.onRequestSuccess " + game.getUrl());
            addBlueBase(game.getUrl());
            addBlueFlag(game.getUrl());

            addRedBase(game.getUrl());
            addRedFlag(game.getUrl());

            finish();
        }
    }
}
