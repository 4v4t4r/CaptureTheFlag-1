package com.blstream.ctfclient.network.service;

import java.util.ArrayList;

/**
 * Created by mar on 24.04.14.
 */
public interface CTFCallback {
    void onGetArrayList(ArrayList<String> result); //on Failure result is null
    void onPostDataResult(String result); //on Failure result is null
}
