package com.blstream.ctfclient.gcm;

import android.app.IntentService;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Bundle;
import android.os.SystemClock;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.blstream.ctfclient.R;
import com.google.android.gms.gcm.GoogleCloudMessaging;

public class GcmIntentService extends IntentService {
    public static final int NOTIFICATION_ID = 1;
    private NotificationManager mNotificationManager;
    NotificationCompat.Builder builder;

    String TAG = "GcmIntentService";

    public GcmIntentService() {
        super("GcmIntentService");
    }

    @Override
    protected void onHandleIntent(Intent intent) {
        Bundle extras = intent.getExtras();
        GoogleCloudMessaging gcm = GoogleCloudMessaging.getInstance(this);
        // The getMessageType() intent parameter must be the intent you received
        // in your BroadcastReceiver.
        String messageType = gcm.getMessageType(intent);

        Log.d(TAG, "onHandleIntent");
        if (!extras.isEmpty()) {  // has effect of unparcelling Bundle
            /*
             * Filter messages based on message type. Since it is likely that GCM
             * will be extended in the future with new message types, just ignore
             * any message types you're not interested in, or that you don't
             * recognize.
             */
            if (GoogleCloudMessaging.
                    MESSAGE_TYPE_SEND_ERROR.equals(messageType)) {
                sendNotification(extras);
            } else if (GoogleCloudMessaging.
                    MESSAGE_TYPE_DELETED.equals(messageType)) {
                sendNotification(extras);
                // If it's a regular GCM message, do some work.
            } else if (GoogleCloudMessaging.
                    MESSAGE_TYPE_MESSAGE.equals(messageType)) {
                Log.i(TAG, "Completed work @ " + SystemClock.elapsedRealtime());
                // Post notification of received message.
                sendNotification(extras);
                Log.i(TAG, "Received: " + extras.toString());
            }
        }
        // Release the wake lock provided by the WakefulBroadcastReceiver.
        GcmBroadcastReceiver.completeWakefulIntent(intent);
    }

    // Put the message into a notification and post it.
    // This is just one simple example of what you might choose to do with
    // a GCM message.
    private void sendNotification(Bundle extras) {
        mNotificationManager = (NotificationManager)
                this.getSystemService(Context.NOTIFICATION_SERVICE);

        String url = extras.getString("url");
        if (url != null) {
            Uri uri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
            String msg = extras.getString("message") + " " + url;
            NotificationCompat.Builder mBuilder =
                    new NotificationCompat.Builder(this)
                            .setSmallIcon(R.drawable.ic_launcher)
                            .setSound(uri)
                            .setContentTitle("CTF")
                            .setStyle(new NotificationCompat.BigTextStyle()
                                    .bigText(msg))
                            .setContentText(msg);

            // pending intent is redirection using the deep-link
            Intent resultIntent = new Intent(Intent.ACTION_VIEW);
            resultIntent.setData(Uri.parse(url));

            PendingIntent pending = PendingIntent.getActivity(this, 0, resultIntent, Intent.FLAG_ACTIVITY_NEW_TASK);
            mBuilder.setContentIntent(pending);

            // using the same tag and Id causes the new notification to replace an existing one
//            mBuilder.setContentIntent(contentIntent);
            mNotificationManager.notify("ELO",NOTIFICATION_ID, mBuilder.build());
        }



    }
}