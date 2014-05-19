package com.blstream.ctfclient.utils;

import com.blstream.ctfclient.model.dto.Location;

import java.util.Random;

/**
 * Created by wde on 2014-05-19.
 */
public class UserLocationSimulation {
    private static final float DELTA = 0.005f;

    private Location mLocation;
    private Random mRandom;

    public UserLocationSimulation(Location location) {
        mLocation = location;
        mRandom = new Random();
    }

    public Location getLocation() {
        return mLocation;
    }

    public void moveRandom() {
        float deltaLon = (mRandom.nextFloat() - 0.5f) * DELTA;
        float deltaLat = (mRandom.nextFloat() - 0.5f) * DELTA;

        mLocation.addToLat(deltaLat);
        mLocation.addToLon(deltaLon);
    }
}
