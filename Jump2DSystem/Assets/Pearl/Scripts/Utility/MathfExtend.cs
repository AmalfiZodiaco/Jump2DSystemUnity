using UnityEngine;

namespace Pearl
{
    /// <summary>
    /// A class tha extend Mathf class
    /// </summary>
    public static class MathfExtend
    {
        /// <summary>
        /// Enumerator that encloses strictly larger and greater-equal symbols
        /// </summary>
        public enum InequalitySign { GraterOrEqual, Greater }

        private static float auxFloat;

        #region Public Methods
        /// <summary>
        /// Returns true if the number is in the range that can be opened or closed(depends on the two signs)
        /// </summary>
        /// <param name = "number"> The number that will be controlled if it is in the range</param>
        /// <param name = "range"> The range</param>
        /// <param name = "firstSign">the lower limit of the range is inclusive or exclusive?</param>
        /// <param name = "secondSign">the upper limit of the range is inclusive or exclusive?</param>
        public static bool IsRange(float number, Range range, InequalitySign firstSign = InequalitySign.GraterOrEqual, InequalitySign secondSign = InequalitySign.GraterOrEqual)
        {
            UnityEngine.Debug.Assert(range.Min <= range.Max);

            switch (firstSign)
            {
                case InequalitySign.GraterOrEqual:
                    switch (secondSign)
                    {
                        case InequalitySign.GraterOrEqual:
                            return number >= range.Min && number <= range.Max;
                        case InequalitySign.Greater:
                            return number >= range.Min && number < range.Max;
                    }
                    break;
                case InequalitySign.Greater:
                    switch (secondSign)
                    {
                        case InequalitySign.GraterOrEqual:
                            return number > range.Min && number <= range.Max;
                        case InequalitySign.Greater:
                            return number > range.Min && number < range.Max;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Returns true if the number is in the range [0-max] that can be opened or closed(depends on the two signs)
        /// </summary>
        /// <param name = "number"> The number that will be controlled if it is in the range</param>
        /// <param name = "max"> The upper limit of the range</param>
        /// <param name = "firstSign">the lower limit of the range is inclusive or exclusive?</param>
        /// <param name = "secondSign">the upper limit of the range is inclusive or exclusive?</param>
        public static bool IsRange(float value, float max, InequalitySign firstSign = InequalitySign.GraterOrEqual, InequalitySign secondSign = InequalitySign.GraterOrEqual)
        {
            UnityEngine.Debug.Assert(max > 0);

            return IsRange(value, new Range(0, max), firstSign, secondSign);
        }

        /// <summary>
        /// Returns true if the number is in the range [0-1] that can be opened or closed(depends on the two signs)
        /// </summary>
        /// <param name = "number"> The number that will be controlled if it is in the range</param>
        /// <param name = "max"> The upper limit of the range</param>
        /// <param name = "firstSign">the lower limit of the range is inclusive or exclusive?</param>
        /// <param name = "secondSign">the upper limit of the range is inclusive or exclusive?</param>
        public static bool IsRange(float value, InequalitySign firstSign = InequalitySign.GraterOrEqual, InequalitySign secondSign = InequalitySign.GraterOrEqual)
        {
            return IsRange(value, new Range(0, 1), firstSign, secondSign);
        }

        /// <summary>
        /// Returns the value from the old range to the new range. The value preserves the proportions(ex: 0.5 in the range between[0, 1] becomes 1.5 in the range [1, 2])
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "originalRange">The original range for the value </param>
        /// <param name = "newRange">The new range for the value </param>
        public static float ChangeRange(float value, Range originalRange, Range newRange)
        {
            UnityEngine.Debug.Assert(value >= originalRange.Min && value <= originalRange.Max);

            auxFloat = (newRange.Max - newRange.Min) / (originalRange.Max - originalRange.Min);
            return newRange.Min + ((value - originalRange.Min) * auxFloat);
        }

        /// <summary>
        /// Returns the value from the old range [0, oldMax] to the new range [0, newMax]. The value preserves the proportions(ex: 0.5 in the range between[0, 1] becomes 1.5 in the range [1, 2])
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "oldMax">The uppper limit of original range for the value </param>
        /// <param name = "newMax">The upper limit of new range for the value </param>
        public static float ChangeRange(float value, float oldMax, float newMax)
        {
            UnityEngine.Debug.Assert(oldMax >= 0 && newMax >= 0 && value >= 0 && value >= oldMax);

            return ChangeRange(value, new Range(0, oldMax), new Range(0, newMax));
        }

        /// <summary>
        /// Returns the value from the old range [0, 1] to the new range [0, newMax]. The value preserves the proportions(ex: 0.5 in the range between[0, 1] becomes 1.5 in the range [1, 2])
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        ///  <param name = "newMax">The upper limit of new range for the value </param>
        public static float ChangeRange(float value, float newMax)
        {
            UnityEngine.Debug.Assert(newMax >= 0 && value >= 0 && value <= 1);
            if (newMax == 0)
                return 0;
            return ChangeRange(value, new Range(0, 1), new Range(0, newMax));
        }

        /// <summary>
        /// Returns the value from the old range [0, 1] to the new range [newMax.x, newMax.y]. 
        /// The value preserves the proportions(ex: 0.5 in the range between[0, 1] becomes 1.5 in the range [1, 2]
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "newRange">The new range for the value </param>
        public static float ChangeRange(float value, Range newRange)
        {
            UnityEngine.Debug.Assert(value >= 0 && value <= 1);

            return ChangeRange(value, new Range(0, 1), newRange);
        }

        /// <summary>
        /// Returns the percent [0, 1] respect a specific range
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "originalRange">The original range for the value </param>
        public static float Percent(float value, Range originalRange)
        {
            UnityEngine.Debug.Assert(value >= originalRange.Min && value <= originalRange.Max);

            return ChangeRange(value, originalRange, new Range(0, 1));
        }

        /// <summary>
        /// Returns the percent [0, 1] respect a specific range [0, max]
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "originalRange">The original range for the value </param>
        public static float Percent(float value, float max)
        {
            UnityEngine.Debug.Assert(value >= 0 && value <= max);

            return ChangeRange(value, new Range(0, max), new Range(0, 1));
        }


        /// <summary>
        /// The method returns the new number which is inserted in a circle of 
        /// number between [0-max)
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "max">The number representing the end of the circle</param>
        public static int ChangeOneInCircle(int value, int max, bool positive)
        {
            UnityEngine.Debug.Assert(max > 0 && value >= 0 && value < max);

            return ChangeOneInCircle(value, 0, max, positive);
        }


        /// <summary>
        /// The method returns the new number which is inserted in a circle of 
        /// number between [min-max)
        /// </summary>
        /// <param name = "value"> The value that will edited</param>
        /// <param name = "min">The number representing the start of the circle</param>
        /// <param name = "max">The number representing the end of the circle</param>
        public static int ChangeOneInCircle(int value, int min, int max, bool positive)
        {
            UnityEngine.Debug.Assert(max > min && value >= min && value < max);

            if (positive)
                value += 1;
            else
                value -= 1;
            if (value < min)
                return max - 1;
            if (value == max)
                return min;
            return value;
        }

        /// <summary>
        /// The method returns the sign of a number
        /// </summary>
        /// <param name = "value"> The value that will be studied to obtain the sign</param>
        public static sbyte Sign(float value)
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
            return 0;
        }
        #endregion
    }
}
