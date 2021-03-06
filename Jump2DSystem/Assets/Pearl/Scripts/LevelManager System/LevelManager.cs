﻿using Pearl.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Level
{
    /// <summary>
    /// The class that implements the generic level manager.
    /// Each level has this class or a derived class (specific by level)
    /// </summary>
    public class LevelManager : LogicalManager
    {
        #region Inspector fields
        /// <summary>
        /// The pool creator prefab.The prefab is used to instantiate and store all objects in the pool
        /// </summary>
        [SerializeField]
        private GameObject poolPrefab = null;
        #endregion

        #region Private fields
        private PauseComponents pauseComponent;
        #endregion

        #region Unity CallBacks
        protected override void Awake()
        {
            base.Awake();
            if (poolPrefab)
            {
                GameObject obj = GameObject.Instantiate(poolPrefab);
                obj.name = obj.name.Split('(')[0];
            }
            EventsManager.AddMethod<genericDelegate<bool>>(EventAction.CallPause, ReceivePause, this);
            pauseComponent = GetLogicalComponent<PauseComponents>();

        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents.Add(typeof(PauseComponents), new PauseComponents());
        }
        #endregion

        #region Interface methods

        #region Receive Methods
        private void ReceivePause(bool pause)
        {
            DoReceivePause(pause);
        }
        #endregion

        #endregion

        #region Logical methods
        private void DoReceivePause(bool pause)
        {
            pauseComponent.DoControlPause(pause);
        }
        #endregion
    }
}
