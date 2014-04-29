package com.blstream.ctfclient.activities;

import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import android.widget.Button;
import android.widget.TextView;

import com.blstream.ctfclient.R;
import com.fourmob.datetimepicker.date.DatePickerDialog;
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

public class CreateGameActivity extends FragmentActivity implements DatePickerDialog.OnDateSetListener, TimePickerDialog.OnTimeSetListener {
    public static final String DATEPICKER_TAG = "datepicker";
    public static final String TIMEPICKER_TAG = "timepicker";

    @InjectView(R.id.game_date_picker_button)
    Button btnSelectDate;
    @InjectView(R.id.game_time_picker_button)
    Button btnSelectTime;
    @InjectView(R.id.games_selected_start_date)
    TextView selectedStartDateTextView;

    static final int DATE_DIALOG_ID = 0;
    static final int TIME_DIALOG_ID = 1;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_game);
        ButterKnife.inject(this);
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
        TimePickerDialog timePickerDialog = TimePickerDialog.newInstance(this, Calendar.getInstance().get(Calendar.HOUR_OF_DAY), calendar.get(Calendar.MINUTE),true, false);
        timePickerDialog.show(getSupportFragmentManager(), TIMEPICKER_TAG);


    }

    private void updateSelectedStartDate(int dialogType, int year, int month, int day, int hour, int minute) {
        Calendar calendar = GregorianCalendar.getInstance();
        calendar.setTimeZone(TimeZone.getDefault());
        String text = selectedStartDateTextView.getText().toString();
        SimpleDateFormat isoFormat = new SimpleDateFormat("dd-MM-yyyy HH:mm", Locale.getDefault());
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

}
