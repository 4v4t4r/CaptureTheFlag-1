package com.blstream.ctfclient.activities;

import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.GridView;

import com.blstream.ctfclient.R;
import com.blstream.ctfclient.adapters.GameAdapter;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.network.requests.CTFGamesRequest;
import com.octo.android.robospice.persistence.DurationInMillis;
import com.octo.android.robospice.persistence.exception.SpiceException;
import com.octo.android.robospice.request.listener.RequestListener;

public class GamesActivity extends CTFBaseActivity {
    GridView gridView;
    private CTFGamesRequest gamesRequest;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.grid_layout);

        gridView = (GridView) findViewById(R.id.grid_view);
        gridView.setAdapter(new GameAdapter(this,getCacheDir()));
    }

    @Override
    protected void onResume() {
        super.onResume();
        gamesRequest = new CTFGamesRequest();
        getSpiceManager().execute(gamesRequest, gamesRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new GamesRequestListener());
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.games, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {

        public PlaceholderFragment() {
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                Bundle savedInstanceState) {
            View rootView = inflater.inflate(R.layout.fragment_games, container, false);
            return rootView;
        }
    }

    private class GamesRequestListener implements RequestListener<Game[]> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.e(GamesActivity.class.getSimpleName(), "Error", spiceException);
        }

        @Override
        public void onRequestSuccess(Game[] games) {
            Log.d(GamesActivity.class.getSimpleName(), ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> JUPI it is work !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for(Game game : games){
                Log.d(GamesActivity.class.getSimpleName(), "########################## GAME ########################################");
                Log.d(GamesActivity.class.getSimpleName(), "name: " + game.getName());
                Log.d(GamesActivity.class.getSimpleName(), "map: " + game.getMap());
                Log.d(GamesActivity.class.getSimpleName(), "description: " + game.getDescription());
                Log.d(GamesActivity.class.getSimpleName(), "url: " + game.getUrl());
            }
        }
    }
}
