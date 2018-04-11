using System;
using System.Collections.Generic;
using Plugin.Geolocator;
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

            var pin = new Pin()
            {
                Type = PinType.Place,
                Address = "fairfield.edu",
                Label = "Fairfield University",
                Position = position
            };


            var locator = CrossGeolocator.Current;

            locator.PositionChanged += (sender, e) => {
                var geoposition = e.Position;
                var userposition = new Position(geoposition.Latitude, geoposition.Longitude);

                this.FindByName<Map>("Map").MoveToRegion(MapSpan.FromCenterAndRadius(userposition, Distance.FromMiles(0.25)));;
            };


            this.FindByName<Map>("Map").Pins.Add(pin);
            this.FindByName<Map>("Map").MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.25)));
        }

        private void SignOut(object sender, System.EventArgs e)
        {
            firebaseAuthenticator.Logout();
            Navigation.PopAsync();
        }
    }
}
