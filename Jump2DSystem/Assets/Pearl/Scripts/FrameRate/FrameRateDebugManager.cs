using UnityEngine.UI;
using UnityEngine;
using Pearl.Events;

namespace Pearl.FrameRate
{
    /// <summary>
    /// The class that writes the frameRate in UI
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class FrameRateDebugManager : LogicalManager
    {
        #region Private Fields
        private Text frameRateText;
        private FrameRateManager frameRateManager;
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
            frameRateText = GetComponent<Text>();
            frameRateManager = EventsManager.GetIstance<FrameRateManager>();
        }

        protected override void Update()
        {
            base.Update();
            DoSeeFPS(frameRateManager.FrameRate);
        }
        #endregion

        #region Logical Methods
        /// <summary>
        /// Writes the framRate in the component text
        /// </summary>
        /// <param name = "FPS"> The current FrameRate</param>
        private void DoSeeFPS(int FPS)
        {
            frameRateText.text = FPS.ToString() + " " + "FPS";
        }
        #endregion
    }
}
