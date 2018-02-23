using System;
using UIKit;

namespace TequiScanner.iOS
{
    public static class Utils
    {
        public static UIColor BlurColor(UIColor color)
        {
            return color.ColorWithAlpha((nfloat)0.7);
        }
    }
}
