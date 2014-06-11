using System;

namespace GestureTouch
{
    public class Range<T> where T : IComparable<T>
    {

        public Range(T mininum, T maximum)
        {
            Minimum = mininum;
            Maximum = maximum;
            if(!IsValid())
                throw new InvalidOperationException("Minimum is not smaller than maximum");
        }
        public T Minimum { get; set; }
        public T Maximum { get; set; }
        public override string ToString() { return String.Format("[{0} - {1}]", Minimum, Maximum); }
        public Boolean IsValid() { return Minimum.CompareTo(Maximum) <= 0; }

        public Boolean ContainsValue(T value)
        {
            return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
        }
        public Boolean IsInsideRange(Range<T> range)
        {
            return IsValid() && range.IsValid() && range.ContainsValue(Minimum) && range.ContainsValue(Maximum);
        }
        public Boolean ContainsRange(Range<T> range)
        {
            return IsValid() && range.IsValid() && ContainsValue(range.Minimum) && ContainsValue(range.Maximum);
        }
    }
}
