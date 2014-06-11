using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GestureTouch
{
    public  class GestureTouchPipeline
    {
        public event EventHandler<GestureTouchEventArgs> GestureTouchDown = delegate { };
        public event EventHandler<GestureTouchEventArgs> GestureTouchMove = delegate { };
        public event EventHandler<GestureTouchEventArgs> GestureTouchUp = delegate { };

        private readonly Dictionary<int,int> _touchInputCounter = new Dictionary<int, int>();

        public int InputFilter { get; set; }
        public double InputSizeScale { get; set; }

        private readonly FrameworkElement _element;
        public List<GestureTouchPoint> Touches
        {
            get { return _touches.Values.ToList(); }
        }

        readonly Dictionary<int, GestureTouchPoint> _touches = new Dictionary<int, GestureTouchPoint>();

        public GestureTouchPipeline(FrameworkElement element)
        {
            _element = element;
            _element.PreviewStylusDown += TouchWindow_PreviewStylusDown;
            _element.PreviewStylusMove += TouchWindow_PreviewStylusMove;
            _element.PreviewStylusUp += TouchWindow_PreviewStylusUp;

            InputFilter = 2;
            InputSizeScale = 1;
        }

        void TouchWindow_PreviewStylusUp(object sender, StylusEventArgs e)
        {
            if (!_touches.ContainsKey(e.StylusDevice.Id))
                return;

            var touchPointSize = TouchDetector.GetSizeFromStylusPoint(e.StylusDevice.GetStylusPoints(_element)[0]);
            touchPointSize.Width    *= InputSizeScale;
            touchPointSize.Height   *= InputSizeScale;
            var touchPoint = new GestureTouchPoint(e.StylusDevice, touchPointSize, e.GetPosition(_element), TouchAction.Up);
            GestureTouchUp(_element, new GestureTouchEventArgs(e.StylusDevice.Id,touchPoint,e.Timestamp));

            _touches.Remove(e.StylusDevice.Id);
        }
        void TouchWindow_PreviewStylusMove(object sender, StylusEventArgs e)
        {
            if (_touchInputCounter[e.StylusDevice.Id]++ <= InputFilter) return;

            _touchInputCounter[e.StylusDevice.Id] = 0;
            if (!_touches.ContainsKey(e.StylusDevice.Id))
            {
                var touchPointSize = TouchDetector.GetSizeFromStylusPoint(e.StylusDevice.GetStylusPoints(_element)[0]);
                touchPointSize.Width *= InputSizeScale;
                touchPointSize.Height *= InputSizeScale;

                var touchPoint = new GestureTouchPoint(e.StylusDevice, touchPointSize, e.GetPosition(_element), TouchAction.Down);

                _touches.Add(e.StylusDevice.Id, touchPoint);

                GestureTouchDown(_element, new GestureTouchEventArgs(e.StylusDevice.Id, touchPoint, e.Timestamp));
            }
            else
            {
                var touchPointSize = TouchDetector.GetSizeFromStylusPoint(e.StylusDevice.GetStylusPoints(_element)[0]);
                touchPointSize.Width *= InputSizeScale;
                touchPointSize.Height *= InputSizeScale;

                var touchPoint = new GestureTouchPoint(e.StylusDevice, touchPointSize, e.GetPosition(_element), TouchAction.Up);
                GestureTouchMove(_element, new GestureTouchEventArgs(e.StylusDevice.Id, touchPoint, e.Timestamp));
            }
        }

        void TouchWindow_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            if (_touchInputCounter.ContainsKey(e.StylusDevice.Id))
                _touchInputCounter[e.StylusDevice.Id] = 0;
            else
                _touchInputCounter.Add(e.StylusDevice.Id, 0);
        }
    }
}
