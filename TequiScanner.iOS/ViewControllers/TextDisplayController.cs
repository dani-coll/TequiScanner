using UIKit;
using TequiScanner.Shared.Model;
using System;

namespace TequiScanner.iOS.ViewControllers
{
    public class TextDisplayController : BaseViewController
    {

        private RecognitionResult _recognition;

        private nfloat _imageHeight;
        private nfloat _imageWidth;

        public TextDisplayController(RecognitionResult analysis, nfloat imageHeight, nfloat imageWidth)
        {
            this._recognition = analysis;
            _imageHeight = imageHeight;
            _imageWidth = imageWidth;
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SetupUI();

        }

        private void SetupUI()
        {
            nfloat screenHeight = UIScreen.MainScreen.Bounds.Height;
            nfloat screenWidth = UIScreen.MainScreen.Bounds.Width;
            nfloat heightRatio = screenHeight / _imageHeight;
            nfloat widthRatio = screenWidth / _imageWidth;


            UIScrollView scrollView = new UIScrollView()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                ScrollEnabled = true
            };

            scrollView.ContentSize = new CoreGraphics.CGSize(_imageWidth * 0.1, _imageHeight * 0.1);
            scrollView.SizeToFit();
            scrollView.Bounces = false;
            scrollView.ContentMode = UIViewContentMode.ScaleAspectFill;

            View.AddSubview(scrollView);
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, View, NSLayoutAttribute.Left, 1, 0));
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, View, NSLayoutAttribute.Right, 1, 0));
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, View, NSLayoutAttribute.Top, 1, 0));
            View.AddConstraint(NSLayoutConstraint.Create(scrollView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, View, NSLayoutAttribute.Bottom, 1, 0));

            View.BackgroundColor = Colors.BackgroundColor;

            foreach (Line line in _recognition.Lines)
            {
                UILabel label = new UILabel()
                {
                    TranslatesAutoresizingMaskIntoConstraints = false,
                    Text = line.Text
                };
                scrollView.AddSubview(label);
                nfloat leftPosition = line.BoundingBox[0];
                nfloat topPosition = line.BoundingBox[1];
                nfloat rightPosition = line.BoundingBox[2];
                nfloat bottomPosition = line.BoundingBox[5];
                scrollView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Left, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Left, 1, leftPosition));
                scrollView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Top, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Top, 1, topPosition));
                scrollView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Right, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Left, 1, rightPosition));
                scrollView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, scrollView, NSLayoutAttribute.Top, 1, bottomPosition));
                scrollView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Right, NSLayoutRelation.LessThanOrEqual, scrollView, NSLayoutAttribute.Right, 1, 0));
                scrollView.AddConstraint(NSLayoutConstraint.Create(label, NSLayoutAttribute.Bottom, NSLayoutRelation.LessThanOrEqual, scrollView, NSLayoutAttribute.Bottom, 1, 0));

            }
        }
    }
}
