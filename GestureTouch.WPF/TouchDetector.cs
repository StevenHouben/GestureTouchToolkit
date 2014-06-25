using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GestureTouch
{
    public static class TouchDetector
    {
        public static Size GetSizeFromStylusPoint(StylusPoint stylusPoint)
        {
            return new Size(
                stylusPoint.GetPropertyValue(StylusPointProperties.Width),
                stylusPoint.GetPropertyValue(StylusPointProperties.Height));
        }

        public static bool HasTouchInput()
        {
            return Tablet.TabletDevices.Cast<TabletDevice>().Any(tabletDevice => tabletDevice.Type == TabletDeviceType.Touch || tabletDevice.Type == TabletDeviceType.Stylus);
        }

        public static string GetInputUnit(StylusPoint stylusPoint)
        {
            return stylusPoint.Description.GetPropertyInfo(StylusPointProperties.Width).Unit.ToString();
        }

        public static Range<double> GetVerticalInputRange(StylusPoint stylusPoint)
        {
            return new Range<double>(
                stylusPoint.Description.GetPropertyInfo(StylusPointProperties.Height).Minimum,
                    stylusPoint.Description.GetPropertyInfo(StylusPointProperties.Height).Maximum);
        }
        public static Range<double> GetHorizontalInputRange(StylusPoint stylusPoint)
        {
            return new Range<double>(
                stylusPoint.Description.GetPropertyInfo(StylusPointProperties.Width).Minimum,
                stylusPoint.Description.GetPropertyInfo(StylusPointProperties.Width).Maximum);
                 
        }
    }
}
