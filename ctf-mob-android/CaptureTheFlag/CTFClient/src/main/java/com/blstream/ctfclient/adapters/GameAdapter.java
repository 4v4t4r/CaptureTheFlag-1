package com.blstream.ctfclient.adapters;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.Point;
import android.os.Build;
import android.util.Log;
import android.view.Display;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.Map;
import com.blstream.ctfclient.network.requests.CTFMapRequest;
import com.octo.android.robospice.SpiceManager;
import com.octo.android.robospice.persistence.DurationInMillis;
import com.octo.android.robospice.persistence.exception.SpiceException;
import com.octo.android.robospice.request.listener.RequestListener;
import com.squareup.picasso.Picasso;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.List;

/**
 * Created by mar on 2/10/14.
 */
public class GameAdapter extends BaseAdapter {

    private final List<Game> games;
    private final SpiceManager spiceManager;
    private Context mContext;
    File cacheDir;
//    String currentURL;
    public String[] mThumbIds = {
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",
            "http://maps.googleapis.com/maps/api/staticmap?center=53.430028,14.553151&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62",

    };

    // Constructor
    public GameAdapter(Context c, File cacheDir, List<Game> games, SpiceManager spiceManager) {
        this.mContext = c;
        this.cacheDir = cacheDir;
        this.games = games;
        this.spiceManager = spiceManager;
    }

    @Override
    public int getCount() {
        return games.size();
    }

    @Override
    public Object getItem(int position) {
        return games.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        WindowManager wm = (WindowManager) mContext.getSystemService(Context.WINDOW_SERVICE);
        Display display = wm.getDefaultDisplay();

        Point size = new Point();
        display.getSize(size);
        int height = size.y;


        int maxH=(height-80)/3/2;
//        currentURL = mThumbIds[position] + "&size="+maxH+"x"+maxH+"&scale=2&sensor=false";

        View rowView = convertView;
        if (rowView == null) {
            LayoutInflater inflater = LayoutInflater.from(mContext);
            rowView = inflater.inflate(R.layout.game_item, null);

            Game game = games.get(position);

            TextView textView = (TextView)rowView.findViewById(R.id.textView);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.append("Name: ").append(game.getName()).append("\n");
            stringBuilder.append("Start time: ");

            String startTime = new SimpleDateFormat("yyyy-MM-dd HH:mm").format(game.getStartTime());
            stringBuilder.append(startTime).append("\n");

            stringBuilder.append("max players: ").append(game.getMaxPlayers()).append("\n");
            stringBuilder.append("players: ").append(game.getPlayers().size()).append("\n");

            textView.setText(stringBuilder.toString()); // 2011-01-18

            ImageView imgAvatar = (ImageView) rowView.findViewById(R.id.imgAvatar);

            if (imgAvatar == null) {
                imgAvatar = new ImageView(mContext);
            }

            CTFMapRequest mapRequest = new CTFMapRequest(1);
            this.spiceManager.execute(mapRequest, mapRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new MapRequestListener(mContext, imgAvatar, maxH));

        }

        return rowView;
    }

    private class MapRequestListener implements RequestListener<Map> {


        private final Context mContext;
        private final ImageView imageView;
        private final int maxH;

        public MapRequestListener(Context mContext, ImageView imgAvatar, int maxH) {
            this.mContext = mContext;
            this.imageView = imgAvatar;
            this.maxH = maxH;
        }

        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.e(GameAdapter.class.getSimpleName(), "Error", spiceException);
            imageView.setImageResource(R.drawable.nomap_img);
        }

        @Override
        public void onRequestSuccess(Map map) {
            String mapImage = String.format("http://maps.googleapis.com/maps/api/staticmap?center=%d,%d&zoom=15&format=png&maptype=roadmap&style=feature:water|color:0x47483e&style=feature:water|element:labels|visibility:off&style=feature:water|element:labels.icon|visibility:off&style=feature:water|element:labels.text.fill|visibility:off&style=feature:transit|element:labels|visibility:off&style=feature:road|visibility:simplified|invert_lightness:true|color:0xe8c3ae|weight:4.2&style=feature:poi|visibility:off&style=feature:road|element:labels|visibility:on|weight:0.1|color:0x080205&style=feature:administrative|element:labels|hue:0xff0000|lightness:100|gamma:1.91|saturation:100|visibility:off&style=feature:transit.line|visibility:off&style=feature:poi.park|element:labels|visibility:off&style=feature:landscape.natural.terrain|visibility:off&style=feature:landscape.man_made|element:labels|visibility:off&style=feature:landscape.natural.terrain|color:0x808080|visibility:off&style=feature:landscape.man_made|visibility:simplified|hue:0x005eff|saturation:1|lightness:-62&size=%dx%d&scale=2&sensor=false", map.getLocation().getLat(), map.getLocation().getLon(), maxH, maxH);

            Picasso.with(mContext).load(mapImage).into(imageView);
        }
    }
}