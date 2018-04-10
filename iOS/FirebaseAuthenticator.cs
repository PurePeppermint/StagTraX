using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;
using stagtrax.iOS;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(FirebaseAuthenticator))]
namespace stagtrax.iOS
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public FirebaseAuthenticator()
        {
            Debug.WriteLine("Creating FirebaseAuthenticator");
        }

        public string GetCurrentUserDisplayName()
        {
            return Auth.DefaultInstance.CurrentUser.DisplayName;
        }

        public string GetCurrentUserEmail()
        {
            return Auth.DefaultInstance.CurrentUser.Email;
        }

        public bool IsLoggedIn()
        {
            if (Auth.DefaultInstance.CurrentUser != null)
            {
                return true;
            }
            return false;
        }

        public bool IsEmailVerified(){
            return Auth.DefaultInstance.CurrentUser.IsEmailVerified;
        }

        public async Task<bool> LoginWithEmailPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInAsync(email, password);
            Debug.WriteLine("Did I wait?");
            if (user != null)
            {
                Debug.WriteLine("Probably yes!");
                return true;
            }
            else
            {
                Debug.WriteLine("Probably not!");
                return false;
            }
        }

        public void Logout()
        {
            NSError error;
            Auth.DefaultInstance.SignOut(out error);
        }

        public async Task<bool> RegisterWithEmailPassword(string email, string password)
        {
            var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
            Debug.WriteLine("Did I wait?");
            if (user != null)
            {
                Debug.WriteLine("Probably yes!");
                return true;
            }
            else
            {
                Debug.WriteLine("Probably not!");
                return false;
            }
        }
    }
}
