using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GestureTouch;
using Microsoft.Surface.Presentation.Controls;

namespace TouchFingerDetection
{
   
    public partial class MainWindow
    {
        readonly Dictionary<int, ScatterViewItem> _touches = new Dictionary<int, ScatterViewItem>();

        private bool _showDetails;
        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;

            var wrap = new GestureTouchPipeline(this);
            wrap.GestureTouchDown += MainWindow_GestureTouchDown;
            wrap.GestureTouchUp += MainWindow_GestureTouchUp;
            wrap.GestureTouchMove += MainWindow_GestureTouchMove;

            KeyUp += MainWindow_KeyUp;
        }

        void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D:
                    _showDetails = true;
                    break;
                case Key.N:
                    _showDetails = false;
                    break;
            }
        }

        void MainWindow_GestureTouchDown(object sender, GestureTouchEventArgs e)
        {
            var item = new ScatterViewItem
            {
                Width = e.TouchPoint.Size.Width * 9.6,
                Height = e.TouchPoint.Size.Height * 9.6,
                Background = Brushes.White,
                Orientation = 0
            };
            item.CanMove = item.CanRotate = item.CanScale = false;
            item.Center = e.TouchPoint.Position;

            

            _touches.Add(e.Id, item);

            View.Items.Add(item);

            UpdateVisualTouchPoint(e.Id, e.TouchPoint);
        }
        void MainWindow_GestureTouchUp(object sender, GestureTouchEventArgs e)
        {
            if (!_touches.ContainsKey(e.Id))
                return;
            View.Items.Remove(_touches[e.Id]);
            _touches.Remove(e.Id);
        }

        void MainWindow_GestureTouchMove(object sender, GestureTouchEventArgs e)
        {
            UpdateVisualTouchPoint(e.Id,e.TouchPoint);
        }

        private void UpdateVisualTouchPoint(int id,GestureTouchPoint point)
        {
            if (!_touches.ContainsKey(id))
                return;
            _touches[id].Width = point.Size.Width*9.6;
            _touches[id].Height = point.Size.Height * 9.6;
            _touches[id].Center = point.Position;

            if (_showDetails)
            {
                if (_touches[id].Content == null)
                {
                    var label = new Label { Content = "", Margin = new Thickness(-60, 0, 0, 0) };
                    _touches[id].Content = label;
                }
                ((Label)_touches[id].Content).Content =
                "x: " + point.Position.X +
                "\ny: " + point.Position.Y +
                "\nw: " + point.Size.Width * 9.6 +
                "\nh: " + point.Size.Height * 9.6;
            }
            else
            {
                if (((Label) _touches[id].Content) != null)
                    _touches[id].Content = null;
            }


            if (TouchRanges.TinyTouch.ContainsValue(_touches[id].Width)
                && TouchRanges.TinyTouch.ContainsValue(_touches[id].Width))
            {
                _touches[id].Background = Brushes.White;
            }

            else if (TouchRanges.SmallTouch.ContainsValue(_touches[id].Width)
                || TouchRanges.SmallTouch.ContainsValue(_touches[id].Width))
            {
                _touches[id].Background = Brushes.Green;
            }
            else if (TouchRanges.MediumTouch.ContainsValue(_touches[id].Width)
                && TouchRanges.MediumTouch.ContainsValue(_touches[id].Width))
            {
                _touches[id].Background = Brushes.Yellow;
            }
            else if (TouchRanges.LargeTouch.ContainsValue(_touches[id].Width)
                && TouchRanges.LargeTouch.ContainsValue(_touches[id].Width))
            {
                _touches[id].Background = Brushes.Orange;
            }
            else if (TouchRanges.VeryLargeTouch.ContainsValue(_touches[id].Width)
                && TouchRanges.VeryLargeTouch.ContainsValue(_touches[id].Width))
            {
                _touches[id].Background = Brushes.Red;
            }
        }
    }

    public class TouchRanges
    {
        public static Range<double> TinyTouch = new Range<double>(0,25);
        public static Range<double> SmallTouch = new Range<double>(26, 50);
        public static Range<double> MediumTouch = new Range<double>(51,100 );
        public static Range<double> LargeTouch = new Range<double>(101, 200);
        public static Range<double> VeryLargeTouch = new Range<double>(201, 1000);
    }
}
