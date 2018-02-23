using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Provider;
using System.IO;
using Android.Content.PM;
using System.Collections.Generic;
using System;
using TequiScanner.Shared.Services;
using Autofac;

namespace TequiScanner.Droid
{
    [Activity]
    public class ScannerResultActivity : Activity
    {
        private string _picturePath;
       // private readonly IComputerVisionService _visionService;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            //_visionService = MainApp.Builder.Resolve<IComputerVisionService>();

            SetContentView(Resource.Layout.ScannerResult);

            // Get picture path from intent extras
            _picturePath = Intent.GetStringExtra("picturePath") ?? string.Empty;
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!string.IsNullOrEmpty(_picturePath))
            {

            }
        }
    }
}

