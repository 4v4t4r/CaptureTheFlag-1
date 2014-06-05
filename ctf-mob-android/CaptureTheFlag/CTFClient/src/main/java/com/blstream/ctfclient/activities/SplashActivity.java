package com.blstream.ctfclient.activities;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.widget.ProgressBar;

import com.blstream.ctfclient.R;
import com.blstream.ctfclient.gcm.InitGCMUtils;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;


public class SplashActivity extends CTFBaseActivity {

    final Handler mHandler = new Handler(Looper.getMainLooper());
    final static int SPLASH_TIMER = 2000;
    final static int SPLASH_TICK_NUMBER = 50;
    final static int SPLASH_UPDATE = SPLASH_TIMER / SPLASH_TICK_NUMBER;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);

        InitGCMUtils.initGCM(getApplicationContext());
    }

    @Override
    protected void onStart() {
        super.onStart();

        final ProgressBar progressBar = (ProgressBar) findViewById(R.id.progressbar);
        progressBar.setProgress(0);

        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                Intent myIntent = new Intent(getBaseContext(), (getToken() != null) ? MainActivity.class : LoginActivity.class);
                startActivity(myIntent);
                finish();
            }
        }, SPLASH_TIMER);

        mHandler.postDelayed(new Runnable() {
            @Override
            public void run() {
                progressBar.setProgress(progressBar.getProgress() + 100 / SPLASH_TICK_NUMBER);
                mHandler.postDelayed(this, SPLASH_UPDATE);
            }
        }, SPLASH_UPDATE);
    }

    private String getToken() {
        return SharedPreferencesUtils.getToken(getApplicationContext());
    }

    @Override
    protected void onStop() {
        super.onStop();

        if (mHandler != null) {
            mHandler.removeCallbacks(null);
            mHandler.removeCallbacksAndMessages(null);
        }
    }
}
