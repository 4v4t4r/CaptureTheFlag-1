package com.blstream.ctfclient.activities;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.GridView;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.adapters.GameAdapter;
import com.blstream.ctfclient.network.requests.CTFRequest;

public class GamesActivity extends Activity {
    GridView gridView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.grid_layout);

         gridView = (GridView) findViewById(R.id.grid_view);

        CTF.getInstance().addToRequestQueue(getGamesRequest());
        gridView.setAdapter(new GameAdapter(this,getCacheDir()));

    }

    private CTFRequest getGamesRequest() {
        return new CTFRequest(
                Request.Method.GET,
                CTF.getInstance().getURL(CTFRequest.REQUEST_PARAM_USERS),
                createSuccessListener(),
                createErrorListener()
        );
    }


    @Override
    protected void onResume() {
        super.onResume();


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

    private Response.ErrorListener createErrorListener() {
        return new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError volleyError) {
//                Toast.makeText(CTF.getStaticApplicationContext(),
//                        ErrorHelper.getMessage(volleyError, CTF.getStaticApplicationContext()),
//                        Toast.LENGTH_LONG).show();
//                Intent myIntent = new Intent(CTF.getStaticApplicationContext(), LoginActivity.class);
//                startActivity(myIntent);
            }
        };
    }

    private Response.Listener<String> createSuccessListener() {
        return new Response.Listener<String>() {
            @Override
            public void onResponse(String jsonObject) {
//                try {
//                    StringBuilder stringBuilder = new StringBuilder();
//                    JSONArray jsonArray = new JSONArray(jsonObject);
//                    for(int i = 0; i < jsonArray.length(); i++){
//                        User user = new Gson().fromJson(jsonArray.getString(i), User.class);
//                        stringBuilder.append("\tUser ").append(i).append("\n");
//                        stringBuilder.append("user name: ").append(user.getUserName()).append("\n");
//                        stringBuilder.append("password: ").append(user.getPassword()).append("\n");
//                        stringBuilder.append("email: ").append(user.getEmail()).append("\n");
//                        stringBuilder.append("nick: ").append(user.getNick()).append("\n");
//                        stringBuilder.append("first name: ").append(user.getFirstName()).append("\n");
//                        stringBuilder.append("last name: ").append(user.getLastName()).append("\n");
//                        if(user.getCoordinates() == null){
//                            stringBuilder.append("location: ").append("").append("\n");
//                        }else{
//                            stringBuilder.append("user name: ").append(user.getCoordinates().toString()).append("\n");
//                        }
//
//                        for(int j = 0; j < user.getCharacter().length; j++){
//                            com.blstream.ctfclient.model.dto.Character character = user.getCharacter()[j];
//                            stringBuilder.append("health: ").append(character.getHealth()).append("\n");
//                            stringBuilder.append("level: ").append(character.getLevel()).append("\n");
//                            stringBuilder.append("total score: ").append(character.getTotalScore()).append("\n");
//                            stringBuilder.append("total time: ").append(character.getTotalTime()).append("\n");
//                            stringBuilder.append("type: ")
//                                    .append(CharacterType.getCharacterTypeNameForValue(character.getType()))
//                                    .append("\n");
//                        }
//                        stringBuilder.append("\n").append("##################").append("\n");
//                    }
////                    ((TextView)getView().findViewById(R.id.users)).setText(stringBuilder.toString());
//                } catch (JSONException e) {
//                    Log.e(CTF.TAG, "Error", e);
//                }
            }
        };
    }

}
