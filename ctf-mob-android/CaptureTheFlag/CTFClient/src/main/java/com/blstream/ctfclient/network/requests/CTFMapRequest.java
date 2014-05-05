package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Map;
import com.blstream.ctfclient.network.interfaces.MapInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by Rafal on 2014-05-05.
 */
public class CTFMapRequest extends RetrofitSpiceRequest<Map, MapInterface> {

    private int mapID;

    public CTFMapRequest(int mapID){
        super(Map.class, MapInterface.class);
    }

    public void setMapID(int mapID) {
        this.mapID = mapID;
    }

    @Override
    public Map loadDataFromNetwork() throws Exception {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().getMap(builder.toString(), mapID);
    }

    public String createCacheKey() {
        return "ctf.map.id." + mapID;
    }

}
