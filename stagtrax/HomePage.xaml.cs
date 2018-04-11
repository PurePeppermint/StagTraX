using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                //this.FindByName<Map>("Map").MoveToRegion(MapSpan.FromCenterAndRadius(userposition, Distance.FromMiles(0.25)));
                Debug.WriteLine("Latitude = '" + geoposition.Latitude + "'");
                Debug.WriteLine("Latitude = '" + geoposition.Longitude + "'");
                Debug.WriteLine("Time = '" + geoposition.Timestamp.ToString() + "'");
                Debug.WriteLine("Heading = '" + geoposition.Heading.ToString() + "'");
                Debug.WriteLine("Speed = '" + geoposition.Speed.ToString() + "'");
                Debug.WriteLine("Accuracy = '" + geoposition.Accuracy.ToString() + "'");
                Debug.WriteLine("Altitude = '" + geoposition.Altitude.ToString() + "'");
                Debug.WriteLine("AltitudeAccuracy = '" + geoposition.AltitudeAccuracy.ToString() + "'");
            };


            this.FindByName<Map>("Map").Pins.Add(pin);
            this.FindByName<Map>("Map").MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.25)));

            locator.StartListeningAsync(TimeSpan.FromSeconds(60.0), 0.0, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            {
                ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                AllowBackgroundUpdates = true,
                DeferLocationUpdates = true,
                DeferralDistanceMeters = 1,
                DeferralTime = TimeSpan.FromSeconds(1),
                ListenForSignificantChanges = true,
                PauseLocationUpdatesAutomatically = false
            });
        }

        private void SignOut(object sender, System.EventArgs e)
        {
            firebaseAuthenticator.Logout();
            Navigation.PopAsync();
        }

        private async void UpdateLocation(object sender, System.EventArgs e)
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Debug.WriteLine("Latitude = '" + position.Latitude + "'");
            Debug.WriteLine("Latitude = '" + position.Longitude + "'");
        }
    }
}
