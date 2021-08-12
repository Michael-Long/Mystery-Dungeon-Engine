using System;

namespace Assets.Utility
{
    public static class MathUtil
    {
        public static bool isBetweenInclusive<T>(T val, T minVal, T maxVal) where T : IComparable<T>
        {
            return minVal.CompareTo(val) <= 0 && val.CompareTo(maxVal) <= 0;
        }
    }
}
