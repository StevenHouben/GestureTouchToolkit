using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestureTouch.Controls
{
    public class TouchVisualizer:UserControl
    {
        readonly Dictionary<int, Border> _touches = new Dictionary<int, Border>();
        readonly Canvas _canvas = new Canvas();

        public Dictionary<Range<double>,Brush> Visualizations = new Dictionary<Range<double>, Brush>();

        public event EventHandler<GestureTouchEventArgs> GestureTouchDown = delegate { };
        public event EventHandler<GestureTouchEventArgs> GestureTouchMove = delegate { };
        public event EventHandler<GestureTouchEventArgs> GestureTouchUp = delegate { };

        public bool ShowTrackingDetails { get; set; }
        public double TouchSizeScale { get; set; }
        public double MinimumSize { get; set; }
        public double MaximumSize { get; set; }

        public TouchVisualizer()
        {
            _canvas.Background = Brushes.Black;
            Content = _canvas;

            Visualizations.Add(DefaultTouchRanges.VeryLargeTouch,Brushes.Red);
            Visualizations.Add(DefaultTouchRanges.LargeTouch, Brushes.Orange);
            Visualizations.Add(DefaultTouchRanges.MediumTouch, Brushes.Yellow);
            Visualizations.Add(DefaultTouchRanges.SmallTouch, Brushes.Green);
            Visualizations.Add(DefaultTouchRanges.TinyTouch, Brushes.White);

            var wrap = new GestureTouchPipeline(this);
            wrap.GestureTouchDown += MainWindow_GestureTouchDown;
            wrap.GestureTouchUp += MainWindow_GestureTouchUp;
            wrap.GestureTouchMove += MainWindow_GestureTouchMove;

            ShowTrackingDetails = true;
            TouchSizeScale = 9.6;
            MinimumSize = 75;
            MaximumSize = 300;
        }
        void MainWindow_GestureTouchDown(object sender, GestureTouchEventArgs e)
        {
            var item = new Border
            {
                Width = e.TouchPoint.Size.Width * TouchSizeScale,
                Height = e.TouchPoint.Size.Height * TouchSizeScale,
                Background = Brushes.White
            };
            _touches.Add(e.Id, item);
            _canvas.Children.Add(item);

            UpdateVisualTouchPoint(e.Id, e.TouchPoint);

            GestureTouchDown(sender, e);
        }
        void MainWindow_GestureTouchUp(object sender, GestureTouchEventArgs e)
        {
            if (!_touches.ContainsKey(e.Id))
                return;

            _canvas.Children.Remove(_touches[e.Id]);
            _touches.Remove(e.Id);

            GestureTouchUp(sender, e);
        }

        void MainWindow_GestureTouchMove(object sender, GestureTouchEventArgs e)
        {
            UpdateVisualTouchPoint(e.Id, e.TouchPoint);
            GestureTouchMove(sender, e);
        }

        private void UpdateVisualTouchPoint(int id, GestureTouchPoint point)
        {
            if (!_touches.ContainsKey(id))
                return;

            _touches[id].Width = point.Size.Width*TouchSizeScale > MinimumSize ? point.Size.Width*TouchSizeScale :MinimumSize;
            _touches[id].Height = point.Size.Height * TouchSizeScale > MinimumSize ? point.Size.Height * TouchSizeScale : MinimumSize;


            Canvas.SetLeft(_touches[id], point.Position.X - _touches[id].Width/2);
            Canvas.SetTop(_touches[id], point.Position.Y - _touches[id].Height/2);

            foreach (var vis in Visualizations.Where(vis => vis.Key.ContainsValue(point.Size.Width * TouchSizeScale)))
                _touches[id].Background = vis.Value;

            if (ShowTrackingDetails)
            {
                if (_touches[id].Child == null)
                {
                    var label = new Label
                    {
                        Content = "", 
                        Margin = new Thickness(-60, 0, 0, 0),
                        Foreground = Brushes.White
                    };
                    _touches[id].Child = label;
                }
                ((Label)_touches[id].Child).Content =
                "x: " + point.Position.X +
                "\ny: " + point.Position.Y +
                "\nw: " + point.Size.Width * TouchSizeScale +
                "\nh: " + point.Size.Height * TouchSizeScale;
            }
            else
            {
                _touches[id].Child = null;
            }
        }
    }
}
