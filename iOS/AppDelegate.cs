using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace stagtrax.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            //FormsMaps.Init();

            Firebase.Core.App.Configure();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
