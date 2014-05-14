package com.blstream.ctfclient.activities;

import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.GridView;
import android.widget.TextView;

import com.blstream.ctfclient.R;
import com.blstream.ctfclient.adapters.GameAdapter;
import com.blstream.ctfclient.model.dto.Game;
import com.blstream.ctfclient.model.dto.Location;
import com.blstream.ctfclient.model.dto.json.RegisterPlayerPositionResponse;
import com.blstream.ctfclient.network.requests.CTFGamesRequest;
import com.blstream.ctfclient.network.requests.CTFRegisterPlayerPositionRequest;
import com.octo.android.robospice.persistence.DurationInMillis;
import com.octo.android.robospice.persistence.exception.SpiceException;
import com.octo.android.robospice.request.listener.RequestListener;

import java.util.Arrays;
import java.util.List;

public class GamesActivity extends CTFBaseActivity implements AdapterView.OnItemClickListener {
    private static final String TAG = GamesActivity.class.getSimpleName();

    private GridView gridView;
    private TextView mGameDetails;
    private CTFGamesRequest gamesRequest;
    private Button mJoinButton;
    private List<Game> mGames;
    private int mSelectedId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.grid_layout);

        gridView = (GridView) findViewById(R.id.grid_view);
        mGameDetails = (TextView) findViewById(R.id.game_details);
        mJoinButton = (Button) findViewById(R.id.button);

        mJoinButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                CTFRegisterPlayerPositionRequest ctfRegisterPlayerPositionRequest = new CTFRegisterPlayerPositionRequest(mGames.get(mSelectedId).getGameId(), new Location(53.447545f, 14.535383f));

                getSpiceManager().execute(ctfRegisterPlayerPositionRequest, ctfRegisterPlayerPositionRequest.createCacheKey(), DurationInMillis.ONE_MINUTE, new RequestListener<RegisterPlayerPositionResponse>() {
                    @Override
                    public void onRequestFailure(SpiceException spiceException) {
                        Log.d(TAG, "onRequestFailure " + spiceException.getLocalizedMessage());
                    }

                    @Override
                    public void onRequestSuccess(RegisterPlayerPositionResponse response) {
                        Log.d(TAG, "onRequestSuccess " + response.toString() + " " + response.getMarkers().size());
                    }
                });
            }
        });
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

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        mGameDetails.setText(mGames.get(position).toString());
        mSelectedId = position;
    }

    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {

        public PlaceholderFragment() {
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            View rootView = inflater.inflate(R.layout.fragment_games, container, false);
            return rootView;
        }
    }

    private class GamesRequestListener implements RequestListener<Game[]> {
        @Override
        public void onRequestFailure(SpiceException spiceException) {
            Log.e(TAG, "Error", spiceException);
        }

        @Override
        public void onRequestSuccess(Game[] games) {

            mGames = Arrays.asList(games);

            gridView.setAdapter(new GameAdapter(getApplicationContext(), R.layout.game_item, mGames));
            gridView.setOnItemClickListener(GamesActivity.this);

            Log.d(TAG, ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> JUPI it is work !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            for (Game game : games) {
                Log.d(TAG, "########################## GAME ########################################");
                Log.d(TAG, "name: " + game.getName());
                Log.d(TAG, "description: " + game.getDescription());
                Log.d(TAG, "url: " + game.getUrl());
                Log.d(TAG, "radius: " + game.getRadius());
                Log.d(TAG, "location: " + game.getLocation());
            }
        }
    }
}
