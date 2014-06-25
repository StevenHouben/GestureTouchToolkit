using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Input;

namespace GestureTouch.Metro
{
    public sealed partial class MainPage : Page
    {
        Windows.UI.Input.GestureRecognizer gr = new Windows.UI.Input.GestureRecognizer();

        public MainPage()
        {
            this.InitializeComponent();
            
            PointerPressed += MainPage_PointerPressed;
            PointerMoved += MainPage_PointerMoved;
            PointerReleased += MainPage_PointerReleased;
            gr.GestureSettings = Windows.UI.Input.GestureSettings.ManipulationRotate | Windows.UI.Input.GestureSettings.ManipulationTranslateX | Windows.UI.Input.GestureSettings.ManipulationTranslateY |
            Windows.UI.Input.GestureSettings.ManipulationScale | Windows.UI.Input.GestureSettings.ManipulationRotateInertia | Windows.UI.Input.GestureSettings.ManipulationScaleInertia |
            Windows.UI.Input.GestureSettings.ManipulationTranslateInertia | Windows.UI.Input.GestureSettings.Tap;
        }

        void MainPage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                gr.ProcessUpEvent(ps[0]);
                e.Handled = true;
                gr.CompleteGesture();
            }
        }

        void MainPage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var a  = e.GetCurrentPoint(this).Properties;
            Text.Text = a.ContactRect.Width + " -- "+a.ContactRect.Height;
            gr.ProcessMoveEvents(e.GetIntermediatePoints(null));
            e.Handled = true;
        }

        void MainPage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var ps = e.GetIntermediatePoints(null);
            if (ps != null && ps.Count > 0)
            {
                gr.ProcessDownEvent(ps[0]);
                e.Handled = true;
            }
        }
    }
}
