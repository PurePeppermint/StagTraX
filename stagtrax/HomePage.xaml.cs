using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace stagtrax
{
    public partial class HomePage : ContentPage
    {
        private IFirebaseAuthenticator firebaseAuthenticator;

        public HomePage(IFirebaseAuthenticator firebaseAuthenticator)
        {
            this.firebaseAuthenticator = firebaseAuthenticator;
            InitializeComponent();
            this.FindByName<Label>("WelcomeLabel").Text = "Signed in as " + firebaseAuthenticator.GetCurrentUserEmail();

            var position = new Position(41.158764, -73.257362);

            var map = new Map(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.25)));

            var pin = new Pin()
            {
                Type = PinType.Place,
                Address = "fairfield.edu",
                Label = "Fairfield University",
                Position = position
            };
        }

        private void SignOut(object sender, System.EventArgs e)
        {
            firebaseAuthenticator.Logout();
            Navigation.PopAsync();
        }
    }
}
