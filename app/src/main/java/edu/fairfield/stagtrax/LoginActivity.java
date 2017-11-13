package edu.fairfield.stagtrax;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.view.View;
import android.widget.EditText;
import android.content.Intent;
import org.apache.commons.validator.routines.EmailValidator;

public class LoginActivity extends AppCompatActivity {
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        final Button loginButton = findViewById(R.id.login);
        final Button resetButton = findViewById(R.id.reset);

        resetButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                restPassword();
            }
        });

        loginButton.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                loginOrRegister();
            }
        });
    }

    private void loginOrRegister() {
        final EditText email = findViewById(R.id.email);
        final EditText password = findViewById(R.id.password);

        if (email.getText().toString().trim().isEmpty()) {
            email.setError("You must enter an email!");
        } else if (!EmailValidator.getInstance().isValid(email.getText().toString())) {
            email.setError("Email is not valid!");
        } else {
            email.setError(null);
            if (password.getText().toString().isEmpty()) {
                Intent intent = new Intent(LoginActivity.this, RegisterActivity.class);
                intent.putExtra("email", email.getText().toString());
                startActivity(intent);
            } else {
                //TODO Validate user
                Intent intent = new Intent(LoginActivity.this, HomeActivity.class);
                startActivity(intent);
            }
        }
    }

    private void restPassword() {
        final EditText email = findViewById(R.id.email);

        if (email.getText().toString().trim().isEmpty()) {
            email.setError("You must enter an email!");
        } else if (!EmailValidator.getInstance().isValid(email.getText().toString())) {
            email.setError("Email is not valid!");
        } else {
            email.setError(null);
            //TODO Reset user's password
        }
    }
}
