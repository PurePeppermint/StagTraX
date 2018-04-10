using System;
using System.Threading.Tasks;
using Firebase.Auth;
using stagtrax.Droid;
using System.Diagnostics;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthenticator))]
namespace stagtrax.Droid
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {

        public FirebaseAuthenticator() {
            this.OnStart();
            Debug.WriteLine("Creating FirebaseAuthenticator");
        }

        protected void OnStart()
        {
            FirebaseAuth.Instance.AuthState += AuthStateChanged;
        }


        protected void OnStop()
        {
            FirebaseAuth.Instance.AuthState -= AuthStateChanged;
        }

        public string GetCurrentUserDisplayName()
        {
            return FirebaseAuth.Instance.CurrentUser.DisplayName;
        }

        public string GetCurrentUserEmail()
        {
            return FirebaseAuth.Instance.CurrentUser.Email;
        }

        public bool IsLoggedIn()
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                return true;
            }
            return false;
        }

        public bool IsEmailVerified()
        {
            return FirebaseAuth.Instance.CurrentUser.IsEmailVerified;
        }

        public async Task<bool> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }

            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                Debug.WriteLine("Login user is not null.");
                return true;
            }
            else
            {
                Debug.WriteLine("Login user is null.");
                return false;
            }
        }

        public async Task<bool> RegisterWithEmailPassword(string email, string password)
        {
            try
            {
                FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }

            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Logout()
        {
            if(FirebaseAuth.Instance.CurrentUser != null)
            {
                FirebaseAuth.Instance.SignOut();
            }
        }

        private void AuthStateChanged(object sender, FirebaseAuth.AuthStateEventArgs e)
        {
            var user = e.Auth.CurrentUser;
            if (user != null)
            {
                Debug.WriteLine("User is signed in as: '" + user.Uid + "'");
            }
            else
            {
                Debug.WriteLine("User is signed out.");
            }
        }
    }
}
