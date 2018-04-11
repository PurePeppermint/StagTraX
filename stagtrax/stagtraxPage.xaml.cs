using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace stagtrax
{
    public partial class stagtraxPage : ContentPage
    {
        private static IFirebaseAuthenticator firebaseAuthenticator;

        public stagtraxPage()
        {
            InitializeComponent();
            firebaseAuthenticator = DependencyService.Get<IFirebaseAuthenticator>();
            SetUI(firebaseAuthenticator.IsLoggedIn());

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetUI(firebaseAuthenticator.IsLoggedIn());
        }

        private void SignIn(object sender, System.EventArgs e)
        {
            Task<bool> success = firebaseAuthenticator.LoginWithEmailPassword(this.FindByName<Entry>("UsernameEntry").Text, this.FindByName<Entry>("PasswordEntry").Text);

            if(success.Result) {
                SetUI(true);
            } else {
                DisplayAlert("Login Failed!", "Double check your username and password and try again.", "Ok");
            }
        }

        private async void Register(object sender, System.EventArgs e)
        {
            bool success = await firebaseAuthenticator.RegisterWithEmailPassword(this.FindByName<Entry>("UsernameEntry").Text, this.FindByName<Entry>("PasswordEntry").Text);
            if(success)
            {
                SetUI(true);
            }
            else
            {
                await DisplayAlert("Registration Failed!", "Unable to create account, verify account doesn;t already exist.", "Ok");
            }
        }

        private void SignOut(object sender, System.EventArgs e)
        {
            firebaseAuthenticator.Logout();
            SetUI(false);
        }

        protected void SetUI(bool SignedIn) 
        {
            if (SignedIn)
            {
                Navigation.PushAsync(new HomePage(firebaseAuthenticator));
            //    this.FindByName<Label>("WelcomeLabel").Text = "Signed in as " + firebaseAuthenticator.GetCurrentUserEmail();
            //    this.FindByName<Entry>("UsernameEntry").Text = "";
            //    this.FindByName<Entry>("UsernameEntry").IsVisible = false;
            //    this.FindByName<Entry>("PasswordEntry").Text = "";
            //    this.FindByName<Entry>("PasswordEntry").IsVisible = false;
            //    this.FindByName<Button>("SignInButton").IsVisible = false;
            //    this.FindByName<Button>("RegisterButton").IsVisible = false;
            //    this.FindByName<Button>("SignOutButton").IsVisible = true;
            }
            else
            {
                this.FindByName<Label>("WelcomeLabel").Text = "Welcome, Sign in...";
                this.FindByName<Entry>("UsernameEntry").Text = "";
                this.FindByName<Entry>("UsernameEntry").IsVisible = true;
                this.FindByName<Entry>("PasswordEntry").Text = "";
                this.FindByName<Entry>("PasswordEntry").IsVisible = true;
                this.FindByName<Button>("SignInButton").IsVisible = true;
                this.FindByName<Button>("RegisterButton").IsVisible = true;
                this.FindByName<Button>("SignOutButton").IsVisible = false;
            }
        }
    }
}
