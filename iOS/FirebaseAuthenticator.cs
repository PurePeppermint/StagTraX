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
			try
			{
    			var user = Auth.DefaultInstance.SignInAsync(email, password);
				//await Task.Delay(2000);
    			Debug.WriteLine("LoginWithEmailPassword getting results");
                if (user != null)
                {
					Debug.WriteLine("true!");
                    return true;
                }
                else
                {
                    Debug.WriteLine("false!");
                    return false;
                }
			}
            catch (NSErrorException ex)
            {
				Debug.WriteLine("LoginWithEmailPassword exception:"+ex.ToString());
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
            var user = Auth.DefaultInstance.CreateUserAsync(email, password);
			//await Task.Delay(2000);
			Debug.WriteLine("RegisterWithEmailPassword getting results");
            if (user != null)
            {
                Debug.WriteLine("true!");
                return true;
            }
            else
            {
                Debug.WriteLine("false!");
                return false;
            }
        }
    }
}
