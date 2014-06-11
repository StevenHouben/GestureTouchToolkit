using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace GestureTouch
{
    public class GestureTouchPoint
    {
        public InputDevice Device { get; set; }
        public Size Size { get; set; }
        public Point Position { get; set; }
        public TouchAction Action { get; set; }

        public Rect Bounds { get; private set; }
        public GestureTouchPoint(InputDevice device, Size size, Point position, TouchAction action)
        {
            Device = device;
            Size = size;
            Position = position;
            Action = action;

            Bounds = new Rect(Position, Size);

        }
        
    }
}
