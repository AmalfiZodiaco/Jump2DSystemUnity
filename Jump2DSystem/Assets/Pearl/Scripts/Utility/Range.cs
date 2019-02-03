using UnityEngine;
using System;

namespace Pearl
{
    /// <summary>
    /// A structure that represents a range of numbers
    /// </summary>
    [Serializable]
    public class Range
    {
        #region private Fields
        [SerializeField]
        private float min;
        [SerializeField]
        private float max;
        #endregion

        public float Min { get { return min; } }
        public float Max { get { return max; } }

        #region Constructors
        public Range(float min, float max)
        {
            Set(min, max);
        }
        #endregion

        #region Set
        public void Set(float min, float max)
        {
            Debug.Assert(min <= max, "the value \"min\" to be less than the value \"max\"");

            this.min = min;
            this.max = max;
        }
        #endregion


        public override string ToString()
        {
            return "[" + min + "," + max + "]";
        }
    }

}