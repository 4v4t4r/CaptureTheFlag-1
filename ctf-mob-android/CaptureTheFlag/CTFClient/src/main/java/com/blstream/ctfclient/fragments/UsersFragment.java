package com.blstream.ctfclient.fragments;

import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.activities.LoginActivity;
import com.blstream.ctfclient.model.dto.Character;
import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.model.enums.CharacterType;
import com.blstream.ctfclient.network.ErrorHelper;
import com.blstream.ctfclient.network.requests.CTFRequest;
import com.google.gson.Gson;

import org.json.JSONArray;
import org.json.JSONException;

/**
 * Created by Rafał Zadrożny on 1/20/14.
 */
public class UsersFragment extends Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_main, container, false);
        return rootView;
    }

    @Override
    public void onViewCreated(View view, Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        CTF.getInstance().addToRequestQueue(getUsersRequest());
    }

    private CTFRequest getUsersRequest() {
        return new CTFRequest(
                    Request.Method.GET,
                    CTF.getInstance().getURL(CTFRequest.REQUEST_PARAM_USERS),
                    createSuccessListener(),
                    createErrorListener()
            );
    }

    private Response.ErrorListener createErrorListener() {
        return new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError volleyError) {
                Toast.makeText(CTF.getStaticApplicationContext(),
                        ErrorHelper.getMessage(volleyError, CTF.getStaticApplicationContext()),
                        Toast.LENGTH_LONG).show();
                Intent myIntent = new Intent(CTF.getStaticApplicationContext(), LoginActivity.class);
                startActivity(myIntent);
            }
        };
    }

    private Response.Listener<String> createSuccessListener() {
        return new Response.Listener<String>() {
            @Override
            public void onResponse(String jsonObject) {
                try {
                    StringBuilder stringBuilder = new StringBuilder();
                    JSONArray jsonArray = new JSONArray(jsonObject);
                    for(int i = 0; i < jsonArray.length(); i++){
                        User user = new Gson().fromJson(jsonArray.getString(i), User.class);
                        stringBuilder.append("\tUser ").append(i).append("\n");
                        stringBuilder.append("user name: ").append(user.getUserName()).append("\n");
                        stringBuilder.append("password: ").append(user.getPassword()).append("\n");
                        stringBuilder.append("email: ").append(user.getEmail()).append("\n");
                        stringBuilder.append("nick: ").append(user.getNick()).append("\n");
                        stringBuilder.append("first name: ").append(user.getFirstName()).append("\n");
                        stringBuilder.append("last name: ").append(user.getLastName()).append("\n");
                        if(user.getCoordinates() == null){
                            stringBuilder.append("location: ").append("").append("\n");
                        }else{
                            stringBuilder.append("user name: ").append(user.getCoordinates().toString()).append("\n");
                        }

                        for(int j = 0; j < user.getCharacter().length; j++){
                            Character character = user.getCharacter()[j];
                            stringBuilder.append("health: ").append(character.getHealth()).append("\n");
                            stringBuilder.append("level: ").append(character.getLevel()).append("\n");
                            stringBuilder.append("total score: ").append(character.getTotalScore()).append("\n");
                            stringBuilder.append("total time: ").append(character.getTotalTime()).append("\n");
                            stringBuilder.append("type: ")
                                    .append(CharacterType.getCharacterTypeNameForValue(character.getType()))
                                    .append("\n");
                        }
                        stringBuilder.append("\n").append("##################").append("\n");
                    }
                    ((TextView)getView().findViewById(R.id.users)).setText(stringBuilder.toString());
                } catch (JSONException e) {
                    Log.e(CTF.TAG, "Error", e);
                }
            }
        };
    }

}
