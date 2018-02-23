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

namespace TequiScanner.Droid
{
    [Activity(Label = "TequiScanner.Droid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private const int _TakePictureRequestCode = 0;

        private Java.IO.File _picturesDirectory;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Check if camera app is installed
            if (IsThereAnAppToTakePictures())
            {
                // Create folder for pictures if not exists
                CreateDirectoryForPictures();

                Button takePhoto = FindViewById<Button>(Resource.Id.takePhotoBtn);
                Button browsePhoto = FindViewById<Button>(Resource.Id.browsePhotoBtn);

                // Set up btn listeners
                takePhoto.Click += delegate
                {
                    // Open camera
                    Intent intent = new Intent(MediaStore.ActionImageCapture);

                    // Save taked picture into given folder
                    var file = new Java.IO.File(
                        _picturesDirectory, 
                        String.Format("scanned_{0}.jpg", Guid.NewGuid()));

                    intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));

                    StartActivityForResult(intent, _TakePictureRequestCode);
                };

                browsePhoto.Click += delegate
                {
                    // Open folder browser
                };
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                switch (requestCode)
                {
                    case _TakePictureRequestCode:
                        // go to scanner result activity
                        Intent intent = new Intent(this, typeof(ScannerResultActivity));
                        intent.PutExtra("picturePath", _picturesDirectory);
                        StartActivity(intent);

                        break;
                }
            }
        }

        private void CreateDirectoryForPictures()
        {
            _picturesDirectory = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures),
                "ScannerPictures");

            if (!_picturesDirectory.Exists())
                _picturesDirectory.Mkdirs();
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
    }
}

