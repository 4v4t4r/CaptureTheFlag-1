package com.blstream.ctfclient.adapters;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Point;
import android.view.Display;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.BaseAdapter;

import com.android.volley.toolbox.DiskBasedCache;
import com.android.volley.toolbox.ImageLoader;
import com.android.volley.toolbox.NetworkImageView;
import com.android.volley.toolbox.Volley;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.utils.BitmapLruCache;
import com.blstream.ctfclient.utils.BitmapUtil;

import java.io.File;

/**
 * Created by mar on 2/10/14.
 */
public class GameAdapter extends BaseAdapter {
    private Context mContext;
    File cacheDir;
    String currentURL;
    public String[] mThumbIds = {
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.428714,14.551722&zoom=14",

    };

    // Constructor
    public GameAdapter(Context c, File cacheDir) {
        mContext = c;
        this.cacheDir = cacheDir;
    }

    @Override
    public int getCount() {
        return mThumbIds.length;
    }

    @Override
    public Object getItem(int position) {
        return mThumbIds[position];
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {


        WindowManager wm = (WindowManager) mContext.getSystemService(Context.WINDOW_SERVICE);
        Display display = wm.getDefaultDisplay();

        Point size = new Point();
        display.getSize(size);
        int width = size.x;
        int height = size.y;


        int maxH=(height-80)/3/2;
        currentURL = mThumbIds[position] + "&size="+maxH+"x"+maxH+"&scale=2&sensor=false";

//        // Initialise Volley Request Queue.
//        mVolleyQueue=Volley.newRequestQueue(this);
//
//        int max_cache_size = 1000000;
//        mImageLoader=new
//
//                ImageLoader(mVolleyQueue, new DiskBitmapCache(getCacheDir(),max_cache_size
//
//        ));
//
//        FadeInImageListener(
//        String testUrlToDownloadImage2 = "http://farm3.static.flickr.com/2848/9110760994_c8dc834397_q.jpg";
//
//
//
//        //1 & 2) are almost same. Demonstrating you can apply animations while showing the downloaded image.
//        // You can use nice entry animations while showing images in a listview.Uses custom implemented 'FadeInImageListener'.
//        mImageLoader.get(testUrlToDownloadImage2,new
//
//                FadeInImageListener(mImageView2, this)
//
//        );

        int max_cache_size = 1000000;

        View rowView = convertView;
        if (rowView == null) {
            LayoutInflater inflater = LayoutInflater.from(mContext);
            rowView = inflater.inflate(R.layout.game_item, null);
        }

        ImageLoader.ImageCache imageCache = new BitmapLruCache(max_cache_size);

        ImageLoader imageLoader = new ImageLoader(Volley.newRequestQueue(mContext), imageCache);


        NetworkImageView imgAvatar = (NetworkImageView) rowView.findViewById(R.id.imgAvatar);

//        imageLoader.get(currentURL, new FadeInImageListener(imgAvatar, mContext));
        imgAvatar.setImageUrl(currentURL, imageLoader);

        return rowView;
    }


    public class DiskBitmapCache extends DiskBasedCache implements ImageLoader.ImageCache {

        public DiskBitmapCache(File rootDirectory, int maxCacheSizeInBytes) {
            super(rootDirectory, maxCacheSizeInBytes);
        }

        public DiskBitmapCache(File cacheDir) {
            super(cacheDir);
        }

        public Bitmap getBitmap(String url) {
            final Entry requestedItem = get(url);

            if (requestedItem == null)
                return null;

            return BitmapFactory.decodeByteArray(requestedItem.data, 0, requestedItem.data.length);
        }

        public void putBitmap(String url, Bitmap bitmap) {

            final Entry entry = new Entry();
            entry.data = BitmapUtil.convertBitmapToBytes(bitmap);
            put(url, entry);
        }
    }


}