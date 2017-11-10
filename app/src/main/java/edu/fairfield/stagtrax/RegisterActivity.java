package edu.fairfield.stagtrax;

import android.app.Activity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.AdapterView;
import android.widget.Toast;
import java.util.Calendar;
import org.apache.commons.validator.routines.EmailValidator;

public class RegisterActivity extends AppCompatActivity {
    private Button button;
    private EditText email;
    private EditText password;
    private EditText confirmPassword;
    private EditText graduationYear;
    private EditText name;
    private Spinner userType;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        button = findViewById(R.id.register);
        email = findViewById(R.id.email);
        password = findViewById(R.id.password);
        confirmPassword = findViewById(R.id.confirm_password);
        graduationYear = findViewById(R.id.graduation_year);
        name = findViewById(R.id.name);
        userType = findViewById(R.id.user_type);

        Bundle bundle = getIntent().getExtras();
        email.setText(bundle.getString("email"));
        userType.setPrompt("User Type");

        userType.setOnItemSelectedListener(new OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                checkUserType();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
                userType.setSelection(0);
            }
        });

        button.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if (validRegistration()) {
                    Toast.makeText(getBaseContext(), "Creating Account...", Toast.LENGTH_LONG).show();
                    //TODO create account here
                    //String email = email.getText().toString();
                    //String password = password.getText().toString();
                    //String year = String.valueOf(userType.getSelectedItem());
                    //String graduationYear = graduationYear.getText().toString();
                    RegisterActivity.this.finish();
                }
            }
        });
    }

    private void checkUserType(){
        if (String.valueOf(userType.getSelectedItem()).equalsIgnoreCase("student")) {
            graduationYear.setVisibility(View.VISIBLE);
        } else {
            graduationYear.setVisibility(View.INVISIBLE);
        }
    }

    private Boolean validRegistration() {
        Boolean continueResult = true;
        if (email.getText().toString().trim().isEmpty()) {
            email.setError("You must enter an email!");
            continueResult = false;
        } else if (!EmailValidator.getInstance().isValid(email.getText().toString())) {
            email.setError("Email is not valid!");
            continueResult = false;
        } else {
            email.setError(null);
        }

        if (password.getText().toString().isEmpty()) {
            confirmPassword.setError(null);
            password.setError("You must enter a password!");
            continueResult = false;
        } else if (password.getText().toString().length() < 6) {
            confirmPassword.setError(null);
            password.setError("Password must be at least 6 characters long!");
            continueResult = false;
        } else if (!password.getText().toString().equals(confirmPassword.getText().toString())) {
            password.setError(null);
            confirmPassword.setError("Passwords must match!");
            continueResult = false;
        } else {
            password.setError(null);
            confirmPassword.setError(null);
        }

        Calendar calandar = Calendar.getInstance();
        if (graduationYear.getText().toString().length() != 4) {
            graduationYear.setError("Enter a 4 digit year!");
            continueResult = false;
        } else if (Integer.parseInt(graduationYear.getText().toString()) < calandar.get(Calendar.YEAR)) {
            graduationYear.setError("You cannot enter a prior year!");
            continueResult = false;
        } else {
            graduationYear.setError(null);
        }

        if (name.getText().toString().isEmpty()) {
            name.setError("Enter a name!");
            continueResult = false;
        } else if (name.getText().toString().length() < 3) {
            name.setError("Enter a longer name!");
            continueResult = false;
        } else {
            name.setError(null);
        }
        return continueResult;
    }
}
