package com.blstream.ctfclient.activities;

import android.support.v4.app.FragmentActivity;

import com.blstream.ctfclient.network.service.CTFRequestSpiceService;
import com.octo.android.robospice.SpiceManager;
/**
 * Created by mar on 25.04.14.
 */
public class CTFBaseActivity extends FragmentActivity {
    private SpiceManager spiceManager = new SpiceManager(CTFRequestSpiceService.class);

    @Override
    protected void onStart() {
        spiceManager.start(this);
        super.onStart();
    }

    @Override
    protected void onStop() {
        spiceManager.shouldStop();
        super.onStop();
    }

    protected SpiceManager getSpiceManager() {
        return spiceManager;
    }

}
