package com.blstream.ctfclient.activities;

import android.app.Fragment;
import android.app.FragmentManager;
import android.graphics.Color;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.network.requests.CTFGameRequest;
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
import java.util.Locale;
import java.util.TimeZone;

import butterknife.ButterKnife;
import butterknife.InjectView;
import butterknife.OnClick;

public class CreateGameActivity extends CTFBaseActivity implements DatePickerDialog.OnDateSetListener, TimePickerDialog.OnTimeSetListener {
    public static final String DATEPICKER_TAG = "datepicker";
    public static final String TIMEPICKER_TAG = "timepicker";
    private GoogleMap googleMap;
    private CTFGameRequest gameRequest;

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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_game);
        ButterKnife.inject(this);
        initView(savedInstanceState);

        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTimeZone(TimeZone.getDefault());
        SimpleDateFormat isoFormat = new SimpleDateFormat(CTF.DATE_FORMAT, Locale.getDefault());

        if (googleMap == null) {
            FragmentManager fragmentManager = getFragmentManager();
            Fragment fragment = fragmentManager.findFragmentById(R.id.map);
            googleMap = ((MapFragment) fragment).getMap();
            googleMap.setOnMapClickListener(new GoogleMap.OnMapClickListener() {
                @Override
                public void onMapClick(LatLng latLng) {
                    Toast.makeText(getApplicationContext(), "Click: " + latLng.latitude + " " + latLng.longitude, Toast.LENGTH_SHORT).show();

                    addFlagToMap(latLng, "Our flag", "Defend your flag against the enemy.", R.drawable.flag_blue);

                    Circle circle = googleMap.addCircle(new CircleOptions()
                            .center(latLng)
                            .radius(10000)
                            .strokeColor(Color.RED)
                            .fillColor(Color.BLUE));
                }
            });


            googleMap.setOnMarkerDragListener(new GoogleMap.OnMarkerDragListener() {
                @Override
                public void onMarkerDragStart(Marker marker) {
                }

                @Override
                public void onMarkerDrag(Marker marker) {
                }

                @Override
                public void onMarkerDragEnd(Marker marker) {
                }
            });
        }
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

        Game game = new Game();
        game.setName(gameNameEditText.getText().toString());
        game.setDescription(gameDescriptionEditText.getText().toString());
        game.setStartTime(selectedStartDateTextView.getText().toString());
        game.setStatus(((Game.GameStatus) gameStatusSpinner.getSelectedItem()));
        game.setType((Game.GameType) gameTypeSpiner.getSelectedItem());

        game.setMaxPlayers(Integer.valueOf(maxPlayerEditText.getText().toString()));
        game.setVisibilityRange(Integer.valueOf(gameVisibilityRangeEditText.getText().toString()));
        game.setActionRange(Integer.valueOf(gameActionRangeEditText.getText().toString()));

        createGame(game);
    }

    private void createGame(final Game game) {
        gameRequest = new CTFGameRequest(game);
        getSpiceManager().execute(gameRequest, gameRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new GameRequestListener());
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
            Toast.makeText(getApplicationContext(), spiceException.getLocalizedMessage(), Toast.LENGTH_SHORT).show();
        }

        @Override
        public void onRequestSuccess(Game game) {
            Toast.makeText(getApplicationContext(), "GameRequestListener success", Toast.LENGTH_SHORT).show();
            finish();
        }
    }
}
