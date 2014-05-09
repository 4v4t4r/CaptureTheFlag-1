package com.blstream.ctfclient.adapters;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.Point;
import android.os.Build;
import android.view.Display;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.Location;
import com.squareup.picasso.Picasso;

import java.util.List;

/**
 * Created by mar on 2/10/14.
 */
public class GameAdapter extends ArrayAdapter<Game> {

    public GameAdapter(Context context, int resource, List<Game> games) {
        super(context, resource, games);
    }

    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        View rowView = convertView;
        ViewHolder viewHolder;

        if (rowView == null) {
            LayoutInflater inflater = LayoutInflater.from(getContext());
            rowView = inflater.inflate(R.layout.game_item, null);

            viewHolder = new ViewHolder();
            viewHolder.gameNameTextView = (TextView) rowView.findViewById(R.id.textView);
            viewHolder.gamePreviewImageView = (ImageView) rowView.findViewById(R.id.imgAvatar);

            rowView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) rowView.getTag();
        }

        Game currentGame = getItem(position);
        viewHolder.gameNameTextView.setText(currentGame.getName());

        imageDownloader(currentGame.getLocation(), viewHolder.gamePreviewImageView);

        return rowView;
    }

    static class ViewHolder {
        public TextView gameNameTextView;
        public ImageView gamePreviewImageView;
    }

    private void imageDownloader(Location location, ImageView imageView) {
        WindowManager wm = (WindowManager) getContext().getSystemService(Context.WINDOW_SERVICE);
        Display display = wm.getDefaultDisplay();

        Point size = new Point();
        display.getSize(size);
        int height = size.y;
        int maxH = (height - 80) / 3 / 2;


        String currentURL = getContext().getString(R.string.single_game_minimap_link_format, maxH, location.getLat(), location.getLon());
        Picasso.with(getContext()).load(currentURL).into(imageView);
    }

}