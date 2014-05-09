package com.blstream.ctfclient.fragments;

import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.activities.CreateGameActivity;
import com.blstream.ctfclient.activities.GamesActivity;
import com.blstream.ctfclient.activities.LoginActivity;
import com.blstream.ctfclient.activities.MapActivity;
import com.blstream.ctfclient.activities.MapWebActivity;
import com.blstream.ctfclient.utils.SharedPreferencesUtils;

/**
 * Created by Rafał Zadrożny on 1/20/14.
 */
public class UsersFragment extends Fragment {

    private View.OnClickListener btnMapListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent myIntent = new Intent(CTF.getStaticApplicationContext(), MapActivity.class);
            startActivity(myIntent);
        }
    };

    private View.OnClickListener btnMapWebListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent myIntent = new Intent(CTF.getStaticApplicationContext(), MapWebActivity.class);
            startActivity(myIntent);
        }
    };

    private View.OnClickListener btnGameListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent myIntent = new Intent(CTF.getStaticApplicationContext(), GamesActivity.class);
            startActivity(myIntent);
        }
    };

    private View.OnClickListener btnLogoutListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            clearToken();
            Intent myIntent = new Intent(CTF.getStaticApplicationContext(), LoginActivity.class);
            myIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TOP);
            getActivity().finish();

            startActivity(myIntent);
        }
    };

    private View.OnClickListener btnCreateGameListener = new View.OnClickListener() {
        @Override
        public void onClick(View view) {
            Intent myIntent = new Intent(CTF.getStaticApplicationContext(), CreateGameActivity.class);
            startActivity(myIntent);
        }
    };

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_users, container, false);
        rootView.findViewById(R.id.map_btn).setOnClickListener(btnMapListener);
        rootView.findViewById(R.id.map_web_btn).setOnClickListener(btnMapWebListener);
        rootView.findViewById(R.id.games_btn).setOnClickListener(btnGameListener);
        rootView.findViewById(R.id.create_game_btn).setOnClickListener(btnCreateGameListener);
        rootView.findViewById(R.id.clear_token_btn).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clearToken();
            }
        });

        rootView.findViewById(R.id.logout_btn).setOnClickListener(btnLogoutListener);
        return rootView;
    }

    private void clearToken() {
        SharedPreferencesUtils.clearToken(getActivity().getApplicationContext());
    }

}
