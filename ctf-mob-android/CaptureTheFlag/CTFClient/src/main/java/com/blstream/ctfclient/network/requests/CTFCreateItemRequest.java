package com.blstream.ctfclient.network.requests;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.model.dto.Item;
import com.blstream.ctfclient.network.interfaces.ItemInterface;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;
import com.octo.android.robospice.request.retrofit.RetrofitSpiceRequest;

import roboguice.util.temp.Ln;

/**
 * Created by wde on 12.05.14.
 */
public class CTFCreateItemRequest extends RetrofitSpiceRequest<Item, ItemInterface> {

    private Item mItem;

    public CTFCreateItemRequest(Item item) {
        super(Item.class, ItemInterface.class);
        mItem = item;
    }

    @Override
    public Item loadDataFromNetwork() {
        Ln.d("Call web service ");
        StringBuilder builder = new StringBuilder("Token ");
        builder.append(SharedPreferencesUtils.getToken(CTF.getStaticApplicationContext()));
        return getService().createItem(builder.toString(), mItem);
    }

    public String createCacheKey() {
        return "CTFCreateItemRequest." + mItem.getName() + " " + mItem.getType();
    }
}
