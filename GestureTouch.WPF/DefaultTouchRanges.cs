namespace GestureTouch
{
    public class DefaultTouchRanges
    {
        public static Range<double> TinyTouch = new Range<double>(0, 25);
        public static Range<double> SmallTouch = new Range<double>(26, 50);
        public static Range<double> MediumTouch = new Range<double>(51, 100);
        public static Range<double> LargeTouch = new Range<double>(101, 200);
        public static Range<double> VeryLargeTouch = new Range<double>(201, 1000);
    }
}
