using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
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
            SetDeviceLocationOnMap();
        }

        private void SignOut(object sender, System.EventArgs e)
        {
            firebaseAuthenticator.Logout();
            Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetDeviceLocationOnMap();
        }

        private async void SetDeviceLocationOnMap()
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                {
                    Debug.WriteLine("Error: Do not have permission to access Location!");
                    return;
                }

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);

                if (position == null)
                {
                    Debug.WriteLine("Error: null position found!");
                    return;
                }
                Debug.WriteLine(string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                                              position.Timestamp, position.Latitude, position.Longitude,
                                              position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed));
                this.FindByName<Map>("Map").MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromMiles(0.3)));
                this.FindByName<Map>("Map").IsShowingUser = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while getting device location:" + ex.ToString());
            }
        }

        private void UpdateAndSaveLocation(object sender, System.EventArgs e)
        {
            SetDeviceLocationOnMap();
        }
    }
}
