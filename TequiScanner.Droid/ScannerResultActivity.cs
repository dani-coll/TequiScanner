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
using System.Drawing;
using Android.Graphics;
using Android.Media;
using Java.IO;
using TequiScanner.Shared.Model;
using System.Linq;

namespace TequiScanner.Droid
{
    [Activity]
    public class ScannerResultActivity : Activity
    {
        private string _picturePath;
        private byte[] _pictureBytes;
        private AnimatedCircleLoadingView _loadingView;
        private TextView _errorView;
        private FrameLayout _textZone;

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
            _errorView = FindViewById<TextView>(Resource.Id.apiErrorMessage);
            _textZone = FindViewById<FrameLayout>(Resource.Id.textZone);
        }
    

        protected async override void OnResume()
        {
            base.OnResume();

            _errorView.Visibility = ViewStates.Gone;
            _loadingView.Visibility = ViewStates.Visible;
            _loadingView.StartIndeterminate();

            if (!string.IsNullOrEmpty(_picturePath))
            {
                _pictureBytes = System.IO.File.ReadAllBytes(_picturePath);

                Bitmap bitmap = await BitmapFactory.DecodeByteArrayAsync(
                    _pictureBytes, 0, _pictureBytes.Length);

                Bitmap cropped = Bitmap.CreateScaledBitmap(bitmap, 1920, 1080, false);
                byte[] byteArray;
                using (var stream = new MemoryStream())
                {
                    await cropped.CompressAsync(Bitmap.CompressFormat.Jpeg, 100, stream);
                    byteArray = stream.ToArray();
                }

                try
                {
                    var result = await _visionService.RecognizeTextService(byteArray);

                    _loadingView.StopOk();
                    _loadingView.Visibility = ViewStates.Gone;

                    if (result == null) throw new Exception("");

                    PrintText(result.Lines);
                }
                catch (Exception e)
                {
                    if (!string.IsNullOrEmpty(e.Message))
                        _errorView.Text = e.Message;

                    _errorView.Visibility = ViewStates.Visible;
                }                
            }
        }

        private void PrintText(List<Line> lines)
        {
            _textZone.RemoveAllViews();
            foreach (var line in lines)
            {
                TextView textView = new TextView(this);

                List<string> words = line.Words.Select(w => w.Text).ToList();
                textView.Text = string.Join(" ", words);

                FrameLayout.LayoutParams layout = new FrameLayout.LayoutParams(textView.Width, textView.Height);
                layout.LeftMargin = line.BoundingBox[0];
                layout.TopMargin = line.BoundingBox[1];

                _textZone.AddView(textView, layout);                
            }
        }
    }
}

