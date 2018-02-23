using System;
using UIKit;

namespace TequiScanner.iOS.Components
{
    public class HighlightedButton : UIButton
    {
        private UIColor defaultColor;

        public HighlightedButton(UIColor defaultColor)
        {
            this.defaultColor = defaultColor;
            BackgroundColor = defaultColor;
        }


        public override bool Highlighted
        {
            get
            {
                return base.Highlighted;
            }
            set
            {
                if (value) BackgroundColor = Utils.BlurColor(BackgroundColor);
                else BackgroundColor = defaultColor;


                base.Highlighted = value;
            }
        }

    }
}

