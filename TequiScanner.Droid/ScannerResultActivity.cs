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
using AnimatedLoadingViews;
using TequiScanner.Shared.Services.Intefaces;

namespace TequiScanner.Droid
{
    [Activity]
    public class ScannerResultActivity : Activity
    {
        private string _picturePath;
        private byte[] _pictureBytes;
        private AnimatedCircleLoadingView _loadingView;

        private IComputerVisionService _visionService;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            using (var scope = MainApp.Container.BeginLifetimeScope())
            {
                _visionService = MainApp.Container.Resolve<IComputerVisionService>();
            }

            SetContentView(Resource.Layout.ScannerResult);

            // Get picture path from intent extras
            _picturePath = Intent.GetStringExtra("picturePath") ?? string.Empty;
            _loadingView = FindViewById<AnimatedCircleLoadingView>(Resource.Id.loadingView);
        }

        protected async override void OnResume()
        {
            base.OnResume();

            _loadingView.StartIndeterminate();

            if (!string.IsNullOrEmpty(_picturePath))
            {
                _pictureBytes = File.ReadAllBytes(_picturePath);
                var result = await _visionService.RecognizeTextService(_pictureBytes);
            }
        }
    }
}

