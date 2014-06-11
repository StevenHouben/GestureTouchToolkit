using System;
using System.Windows;
using System.Windows.Input;

namespace GestureTouch
{
    public class GestureTouchEventArgs:EventArgs
    {
        public int Id { get; private set; }

        public GestureTouchPoint TouchPoint { get; private set; }

        public int Timestamp { get; private set; }

        public GestureTouchEventArgs(int id, GestureTouchPoint point, int time)
        {
            Id = id;
            TouchPoint = point;
            Timestamp = time;
        }
    }
}
