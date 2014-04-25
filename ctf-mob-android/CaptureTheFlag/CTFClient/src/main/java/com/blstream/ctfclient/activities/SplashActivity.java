package com.blstream.ctfclient.activities;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.preference.PreferenceManager;
import android.widget.ProgressBar;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.constants.CTFConstants;


public class SplashActivity extends CTFBaseActivity {

    final Handler mHandler = new Handler(Looper.getMainLooper());
    final static int SPLASH_TIMER = 2000;
    final static int SPLASH_TICK_NUMBER = 50;
    final static int SPLASH_UPDATE = SPLASH_TIMER / SPLASH_TICK_NUMBER;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
        final ProgressBar progressBar = (ProgressBar) findViewById(R.id.progressbar);
        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                Intent myIntent = new Intent(getBaseContext(), (getToken() != null) ? MainActivity.class : LoginActivity.class);
                startActivity(myIntent);
                finish();
            }
        },SPLASH_TIMER);

        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                progressBar.setProgress(progressBar.getProgress() + 100 / SPLASH_TICK_NUMBER);
                mHandler.postDelayed(this, SPLASH_UPDATE);
            }
        }, SPLASH_UPDATE);
    }

    private String getToken() {
        SharedPreferences sharedPreferences = PreferenceManager.getDefaultSharedPreferences(CTF.getStaticApplicationContext());
        return sharedPreferences.getString(CTFConstants.ACCESS_TOKEN_KEY_NAME, null);
    }
    @Override
    protected void onPause() {
        super.onPause();

        if(mHandler != null){
            mHandler.removeCallbacks(null);
            mHandler.removeCallbacksAndMessages(null);
        }
    }
}
