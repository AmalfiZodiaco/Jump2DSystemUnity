using UnityEngine;
using Pearl.Clock;
using UnityEngine.Audio;

namespace Pearl.Audio
{
    /// <summary>
    /// A class that represents a mixer channel. In this class the volume of the 
    /// channel can be increased or decreased.
    /// </summary>
    public class AudioContainer
    {
        #region Readonly Fields
        private readonly Timer timer;
        private readonly Range volumeRange;
        private readonly static Range lerpRange = new Range(0, 1);
        #endregion

        #region Private Fields
        private AnimationCurve curve;
        private bool decrease;
        #endregion

        #region Auxiliar Fields
        private float auxVolume;
        #endregion

        #region Properties
        /// <summary>
        /// The name of channel of mixer
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region Constructors
        public AudioContainer(string name)
        {
            this.Name = name;
            volumeRange = new Range(0, 1);
            this.timer = new Timer();
        }
        #endregion

        #region Obey Methods
        /// <summary>
        /// Creates new volume transition
        /// </summary>
        /// <param name = "volumeActualValue">The actual volume of the channel mixer</param>
        /// <param name = "volumeNewValue">The voluem to be reached</param>
        /// <param name = "time"> Time for volume transition</param>
        /// <param name = "curve">The transition curve.If the curve does not exist, the volume change is linear, if it exists, the change follows the curve.</param>
        public float ObeyReset(float volumeActualValue, float volumeNewValue, float time, AnimationCurve curve = null)
        {
            UnityEngine.Debug.Assert(time >= 0);

            if (volumeActualValue > volumeNewValue)
            {
                volumeRange.Set(volumeNewValue, volumeActualValue);
                decrease = true;
            }
            else
            {
                volumeRange.Set(volumeActualValue, volumeNewValue);
                decrease = false;
            }
            this.timer.ResetOn(time);
            this.curve = curve;
            return ObeyReturnVolume();
        }

        /// <summary>
        /// Returns the volume during the transition
        /// </summary>
        public float ObeyReturnVolume()
        {
            auxVolume = Evalutate();
            if (decrease)
                auxVolume = 1 - auxVolume;
            return MathfExtend.ChangeRange(auxVolume, lerpRange, volumeRange);
        }

        /// <summary>
        /// Returns if the volume transition is over
        /// </summary>
        public bool ObeyIsFinish()
        {
            UnityEngine.Debug.Assert(timer != null);

            return timer.IsFinish();
        }
        #endregion

        #region Private Methods
        private float Evalutate()
        {
            if (curve != null)
                return curve.Evaluate(timer.TimeInPercent);
            else
                return timer.TimeInPercent;
        }
        #endregion
    }
}
