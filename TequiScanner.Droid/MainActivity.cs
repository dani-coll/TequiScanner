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
        private const int _BrowsePictureRequestCode = 1;

        private Java.IO.File _picturesDirectory;
        private string _pictureName;

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
                    _pictureName = String.Format("scanned_{0}.jpg", Guid.NewGuid());
                    var file = new Java.IO.File(
                        _picturesDirectory,
                        _pictureName);

                    intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));

                    StartActivityForResult(intent, _TakePictureRequestCode);
                };

                browsePhoto.Click += delegate
                {
                    // Open folder browser
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(
                        Intent.CreateChooser(imageIntent, "Select photo"),
                        _BrowsePictureRequestCode);
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

                        string fullPath = Path.Combine(_picturesDirectory.AbsolutePath, _pictureName);
                        intent.PutExtra("picturePath", fullPath);
                        StartActivity(intent);

                        break;
                    case _BrowsePictureRequestCode:
                        Intent intent2 = new Intent(this, typeof(ScannerResultActivity));
                        intent2.PutExtra("picturePath", data.Data.Path);
                        StartActivity(intent2);
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

