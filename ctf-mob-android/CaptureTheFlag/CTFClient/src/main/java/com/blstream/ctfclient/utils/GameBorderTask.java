package com.blstream.ctfclient.utils;

import android.os.AsyncTask;

import com.blstream.ctfclient.R;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.PolygonOptions;
import com.google.maps.android.SphericalUtil;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by wde on 2014-05-15.
 */
public class GameBorderTask extends AsyncTask<Void, Void, Void> {

    private PolygonOptions mPolygonOptions;
    private List<LatLng> mHole;
    private List<LatLng> mBorder;
    private LatLng mCenterPoint;
    private double mDistance;
    private OnGameBorderTaskListener mOnGameBorderTaskListener;

    public GameBorderTask(LatLng centerPoint, double distance, OnGameBorderTaskListener onGameBorderTaskListener) {
        mHole = new ArrayList<>();
        mBorder = new ArrayList<>();

        mCenterPoint = centerPoint;
        mDistance = distance;

        mOnGameBorderTaskListener = onGameBorderTaskListener;
    }

    @Override
    protected Void doInBackground(Void... voids) {
        for (int degree = 0; degree < 360; degree++) {
            mHole.add(SphericalUtil.computeOffset(mCenterPoint, mDistance, degree));
            mBorder.add(SphericalUtil.computeOffset(mCenterPoint, mDistance * 50, degree));
        }

        mHole.add(mHole.get(0));
        mBorder.add(mBorder.get(0));

        return (null);
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        mOnGameBorderTaskListener.onGameBorderTaskEnd(new PolygonOptions()
                .addAll(mBorder)
                .addHole(mHole)
                .fillColor(R.color.transparent_black)
                .strokeColor(R.color.transparent_black)
                .strokeWidth(8.0f)
                .visible(true));
    }

    public interface OnGameBorderTaskListener {
        public void onGameBorderTaskEnd(PolygonOptions border);
    }
}
