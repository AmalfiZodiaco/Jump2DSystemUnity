﻿using Pearl.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Pearl.DebugMode
{
    /// <summary>
    /// The singleton class handles debug writing in an UI.The class is activated only in the build version.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class DebugBuildManager : LogicalManager, ISingleton
    {
        #region Private Fields
        private Text textComponent;
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
            textComponent = GetComponentInChildren<Text>();
        }
        #endregion

        #region Singleton Methods
        /// <summary>
        /// Writes Log in the component Text
        /// </summary>
        /// <param name = "obj">The object that will be written</param>
        public void Log(object obj)
        {
            DoLog(obj);
        }
        #endregion

        #region Logical Methods
        private void DoLog(object obj)
        {
            textComponent.text += obj.ToString() + "\n";
        }
        #endregion
    }
}
