using UIKit;
namespace TequiScanner.iOS.ViewControllers
{
    public class TextDisplayController : BaseViewController
    {
        public TextDisplayController()
        {
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SetupUI();

        }

        private void SetupUI() {
            View.BackgroundColor = Colors.BackgroundColor;
        }
    }
}
