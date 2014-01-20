package com.blstream.ctfclient.activities;

import android.animation.Animator;
import android.animation.AnimatorListenerAdapter;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.View;
import android.view.inputmethod.EditorInfo;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.blstream.ctfclient.CTF;
import com.blstream.ctfclient.R;
import com.blstream.ctfclient.model.dto.User;
import com.blstream.ctfclient.network.ErrorHelper;
import com.blstream.ctfclient.network.requests.RegisterRequest;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

/**
 * Created by Rafal on 10.01.14.
 */
public class RegisterActivity extends Activity {

    /**
     * The default username to populate the username field with.
     */
    public static final String EXTRA_USERNAME = "com.example.android.authenticatordemo.extra.USERNAME";
    public static final String REQUEST_TAG = "register";

    /**
     * Keep track of the login task to ensure we can cancel it if requested.
     */
    private UserRegisterTask mAuthTask = null;

    // Values for email and password at the time of the login attempt.
    private String mName;
    private String mEmail;
    private String mPassword;
    private String mRepeat;

    // UI references.
    private EditText mEmailView;
    private EditText mPasswordView;
    private EditText mRepeatPassword;
    private EditText mUserName;
    private View mRegisterFormView;
    private View mRegisterStatusView;
    private TextView mLoginStatusMessageView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_register);

        // Set up the login form.
        mUserName = (EditText) findViewById(R.id.user_name);

        mEmail = getIntent().getStringExtra(EXTRA_USERNAME);
        mEmailView = (EditText) findViewById(R.id.email);
        mEmailView.setText(mEmail);

        mPasswordView = (EditText) findViewById(R.id.password);
        mPasswordView.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView textView, int id, KeyEvent keyEvent) {
                if (id == R.id.login || id == EditorInfo.IME_NULL) {
                    attemptRegister();
                    return true;
                }
                return false;
            }
        });
        mRepeatPassword = (EditText) findViewById(R.id.repeat_password);

        mRegisterFormView = findViewById(R.id.register_form);
        mRegisterStatusView = findViewById(R.id.register_status);
        mLoginStatusMessageView = (TextView) findViewById(R.id.register_status_message);

        findViewById(R.id.register_button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                attemptRegister();
            }
        });
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        super.onCreateOptionsMenu(menu);
        getMenuInflater().inflate(R.menu.login, menu);
        return true;
    }

    /**
     * Attempts to sign in or register the account specified by the login form.
     * If there are form errors (invalid email, missing fields, etc.), the
     * errors are presented and no actual login attempt is made.
     */
    public void attemptRegister() {
        if (mAuthTask != null) {
            return;
        }

        // Reset errors.
        mUserName.setError(null);
        mEmailView.setError(null);
        mPasswordView.setError(null);
        mRepeatPassword.setError(null);

        // Store values at the time of the login attempt.
        mName = mUserName.getText().toString();
        mEmail = mEmailView.getText().toString();
        mPassword = mPasswordView.getText().toString();
        mRepeat = mRepeatPassword.getText().toString();

        boolean cancel = false;
        View focusView = null;

        // Check for a valid password.
        if (TextUtils.isEmpty(mName)) {
            mUserName.setError(getString(R.string.error_field_required));
            focusView = mUserName;
            cancel = true;
        } else if (mName.length() < 4) {
            mUserName.setError(getString(R.string.error_invalid_password));
            focusView = mUserName;
            cancel = true;
        }

        // Check for a valid password.
        if (TextUtils.isEmpty(mPassword)) {
            mPasswordView.setError(getString(R.string.error_field_required));
            focusView = mPasswordView;
            cancel = true;
        } else if (mPassword.length() < 4) {
            mPasswordView.setError(getString(R.string.error_invalid_password));
            focusView = mPasswordView;
            cancel = true;
        }

        // Check for a valid repeated password.
        if (TextUtils.isEmpty(mRepeat)) {
            mRepeatPassword.setError(getString(R.string.error_field_required));
            focusView = mPasswordView;
            cancel = true;
        } else if (!mRepeat.equals(mPassword)) {
            mRepeatPassword.setError(getString(R.string.error_invalid_repeated_password));
            focusView = mPasswordView;
            cancel = true;
        }

        // Check for a valid email address.
        if (TextUtils.isEmpty(mEmail)) {
            mEmailView.setError(getString(R.string.error_field_required));
            focusView = mEmailView;
            cancel = true;
        } else if (!mEmail.contains("@")) {
            mEmailView.setError(getString(R.string.error_invalid_email));
            focusView = mEmailView;
            cancel = true;
        }

        if (cancel) {
            // There was an error; don't attempt login and focus the first
            // form field with an error.
            focusView.requestFocus();
        } else {
            // Show a progress spinner, and kick off a background task to
            // perform the user login attempt.
            mLoginStatusMessageView.setText(R.string.register_progress);
            showProgress(true);
            mAuthTask = new UserRegisterTask();
            mAuthTask.execute((Void) null);
        }
    }

    /**
     * Shows the progress UI and hides the login form.
     */
    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
    private void showProgress(final boolean show) {
        // On Honeycomb MR2 we have the ViewPropertyAnimator APIs, which allow
        // for very easy animations. If available, use these APIs to fade-in
        // the progress spinner.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB_MR2) {
            int shortAnimTime = getResources().getInteger(android.R.integer.config_shortAnimTime);

            mRegisterStatusView.setVisibility(View.VISIBLE);
            mRegisterStatusView.animate()
                    .setDuration(shortAnimTime)
                    .alpha(show ? 1 : 0)
                    .setListener(new AnimatorListenerAdapter() {
                        @Override
                        public void onAnimationEnd(Animator animation) {
                            mRegisterStatusView.setVisibility(show ? View.VISIBLE : View.GONE);
                        }
                    });

            mRegisterFormView.setVisibility(View.VISIBLE);
            mRegisterFormView.animate()
                    .setDuration(shortAnimTime)
                    .alpha(show ? 0 : 1)
                    .setListener(new AnimatorListenerAdapter() {
                        @Override
                        public void onAnimationEnd(Animator animation) {
                            mRegisterFormView.setVisibility(show ? View.GONE : View.VISIBLE);
                        }
                    });
        } else {
            // The ViewPropertyAnimator APIs are not available, so simply show
            // and hide the relevant UI components.
            mRegisterStatusView.setVisibility(show ? View.VISIBLE : View.GONE);
            mRegisterFormView.setVisibility(show ? View.GONE : View.VISIBLE);
        }
    }

    private void startLoginActivity() {
        Intent myIntent = new Intent(getBaseContext(), LoginActivity.class);
        myIntent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        startActivity(myIntent);
    }

    private Response.ErrorListener createRegisterErrorListener() {
        return new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError volleyError) {
                showProgress(false);
                Toast.makeText(getApplicationContext(), ErrorHelper.getMessage(volleyError, getApplicationContext()),
                        Toast.LENGTH_LONG).show();
                startLoginActivity();
            }
        };
    }

    private Response.Listener<String> createRegisterSuccessListener() {
        return new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                showProgress(false);
                Toast.makeText(getApplicationContext(), "User created:\n\n" + response, Toast.LENGTH_LONG).show();
                startLoginActivity();
            }
        };
    }

    /**
     * Represents an asynchronous login/registration task used to authenticate
     * the user.
     */
    public class UserRegisterTask extends AsyncTask<Void, Void, Boolean> {
        @Override
        protected Boolean doInBackground(Void... params) {
            CTF.getInstance().addToRequestQueue(getRegisterRequest());
            return true;
        }

        private RegisterRequest getRegisterRequest() {
            return new RegisterRequest(
                            Request.Method.POST,
                            CTF.getInstance().getURL(RegisterRequest.URL_REQUEST),
                            getUserDataAsJson(),
                            createRegisterSuccessListener(),
                            createRegisterErrorListener()
                    );
        }

        private String getUserDataAsJson() {
            User user = new User();
            user.setEmail(mEmailView.getText().toString());
            user.setUserName(mUserName.getText().toString());
            user.setPassword(mPasswordView.getText().toString());
            Gson gson = new GsonBuilder().create();
            String json = gson.toJson(user);
            return json;
        }

        @Override
        protected void onPostExecute(final Boolean success) {
            mAuthTask = null;

            if (success) {
                //
            } else {
                mPasswordView.setError(getString(R.string.error_incorrect_password));
                mPasswordView.requestFocus();
            }
        }

        @Override
        protected void onCancelled() {
            mAuthTask = null;
            showProgress(false);
        }
    }

}
