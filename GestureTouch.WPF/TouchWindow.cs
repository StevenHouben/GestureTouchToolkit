using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GestureTouch
{
    public class TouchWindow:Window
    {
        public event EventHandler<GestureTouchEventArgs> GestureTouchDown = delegate { };
        public event EventHandler<GestureTouchEventArgs> GestureTouchMove = delegate { };
        public event EventHandler<GestureTouchEventArgs> GestureTouchUp = delegate { };

        public List<GestureTouchPoint> Touches
        {
            get { return _touches.Values.ToList(); }
        }

        readonly ConcurrentDictionary<int, GestureTouchPoint> _touches = new ConcurrentDictionary<int, GestureTouchPoint>();

        public TouchWindow()
        {
            PreviewStylusDown += TouchWindow_PreviewStylusDown;
            PreviewStylusMove += TouchWindow_PreviewStylusMove;
            PreviewStylusUp += TouchWindow_PreviewStylusUp;
        }

        void TouchWindow_PreviewStylusUp(object sender, StylusEventArgs e)
        {
            if (!_touches.ContainsKey(e.StylusDevice.Id))
                return;

            var touchPointSize = TouchDetector.GetSizeFromStylusPoint(e.StylusDevice.GetStylusPoints(this)[0]);

            var touchPoint = new GestureTouchPoint(e.StylusDevice, touchPointSize, e.GetPosition(this), TouchAction.Up);
            GestureTouchUp(this, new GestureTouchEventArgs(e.StylusDevice.Id,touchPoint,e.Timestamp));

            GestureTouchPoint dump;
            _touches.TryRemove(e.StylusDevice.Id, out dump);
        }
        void TouchWindow_PreviewStylusMove(object sender, StylusEventArgs e)
        {
            var touchPointSize = TouchDetector.GetSizeFromStylusPoint(e.StylusDevice.GetStylusPoints(this)[0]);
            var touchPoint = new GestureTouchPoint(e.StylusDevice, touchPointSize, e.GetPosition(this), TouchAction.Up);
            GestureTouchMove(this, new GestureTouchEventArgs(e.StylusDevice.Id, touchPoint, e.Timestamp));
        }

        void TouchWindow_PreviewStylusDown(object sender, StylusDownEventArgs e)
        {
            var touchPointSize = TouchDetector.GetSizeFromStylusPoint(e.StylusDevice.GetStylusPoints(this)[0]);

            var touchPoint = new GestureTouchPoint(e.StylusDevice, touchPointSize, e.GetPosition(this), TouchAction.Down);

            _touches.AddOrUpdate(e.StylusDevice.Id, touchPoint,
                (key, oldValue) => touchPoint);

            GestureTouchDown(this,new GestureTouchEventArgs(e.StylusDevice.Id,touchPoint,e.Timestamp));
        }
    }
    
}
