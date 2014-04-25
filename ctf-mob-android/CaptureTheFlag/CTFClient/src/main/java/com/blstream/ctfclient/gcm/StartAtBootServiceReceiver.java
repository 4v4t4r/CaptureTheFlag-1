package com.blstream.ctfclient.gcm;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/**
 * Created by marcin on 05.03.14.
 */
public class StartAtBootServiceReceiver extends BroadcastReceiver{
    @Override
    public void onReceive(Context context, Intent intent)
    {
            Intent i = new Intent();
            i.setAction("com.blstream.ctfclient.gcm.GCMService");
            context.startService(i);

    }
}