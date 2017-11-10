package edu.fairfield.stagtrax;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.Button;
import android.view.View;
import android.widget.EditText;
import android.content.Intent;
//import android.widget.Toast;
import org.apache.commons.validator.routines.EmailValidator;

public class LoginActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        final Button button = findViewById(R.id.login);
        final EditText email = findViewById(R.id.email);
        final EditText password = findViewById(R.id.password);
        button.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if (email.getText().toString().trim().isEmpty()) {
                    email.setError("You must enter an email!");
                } else if (!EmailValidator.getInstance().isValid(email.getText().toString())) {
                    email.setError("Email is not valid!");
                } else {
                    email.setError(null);
                    if (password.getText().toString().isEmpty()) {
                        password.setError(null);
                        Intent intent = new Intent(LoginActivity.this, RegisterActivity.class);
                        intent.putExtra("email", email.getText().toString());
                        startActivity(intent);
                    } else {
                        //Toast.makeText(getBaseContext(), "Login is not implemented!", Toast.LENGTH_LONG).show();
                        //TODO Validate user then show main activity
                    }
                }
            }
        });
    }
}
