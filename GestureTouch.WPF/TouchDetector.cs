using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GestureTouch
{
    public static class TouchDetector
    {
        public static double CentimeterPerInch = 2.54d;
        public static double PixelPerInch = 96d;

        public static Size GetSizeFromStylusPoint(StylusPoint stylusPoint)
        {
            return new Size(
                stylusPoint.GetPropertyValue(StylusPointProperties.Width),
                stylusPoint.GetPropertyValue(StylusPointProperties.Height));
        }

        public static double ComputeSizeScaleFactorFormReportedUnit(StylusPoint stylusPoint)
        {
            var unit = stylusPoint.Description.GetPropertyInfo(StylusPointProperties.Width).Unit;
            var scale = 0.0;
            switch (unit)
            {
                case StylusPointPropertyUnit.Centimeters:
                    scale = CentimeterPerInch/PixelPerInch ;
                    break;
                case StylusPointPropertyUnit.None:
                    scale =  (CentimeterPerInch*10)/PixelPerInch;
                    break;
                default:
                    scale = PixelPerInch;
                    break;
            }
            return scale;
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
