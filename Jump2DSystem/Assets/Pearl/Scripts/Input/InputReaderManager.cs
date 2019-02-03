using System.Collections.Generic;
using UnityEngine;
using Pearl.Events;
using System;

namespace Pearl.InputSystem
{
    /// <summary>
    /// The class that manages the input. In the update, it calls the 
    /// component that actually reads the input based on the type of controller
    /// </summary>
    public class InputReaderManager : LogicalManager
    {
        #region private fields
        protected ControllerEnum controller = ControllerEnum.PC;
        private readonly Dictionary<ControllerEnum, Action> stringToAction = new Dictionary<ControllerEnum, Action>();
        private InputReaderComponent inputComponent;
        #endregion

        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();
            inputComponent = GetLogicalComponent<InputReaderComponent>();
            EventsManager.AddMethod<genericDelegate<bool>>(EventAction.CallPause, ReceivePause, this);
            CreateDictionary();
        }

        protected override void Update()
        {
            base.Update();
            stringToAction[controller].Invoke();
        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents.Add(typeof(InputReaderComponent), new InputReaderComponent());
        }

        private void CreateDictionary()
        {
            stringToAction.Add(ControllerEnum.PC, inputComponent.DoUpdateKeyboard);
            stringToAction.Add(ControllerEnum.OTHER, inputComponent.DoUpdateKeyboard);
            stringToAction.Add(ControllerEnum.SONY, inputComponent.DoUpdateJoystick);
            stringToAction.Add(ControllerEnum.XBOX, inputComponent.DoUpdateJoystick);
        }
        #endregion

        #region Interface Methods

        #region Receive Methods
        private void ReceivePause(bool pause)
        {
            DoReceivePause(pause);
        }
        #endregion

        #endregion

        #region Logical methods
        protected void DoChangeController(ControllerEnum controller)
        {
            this.controller = controller;
            EventsManager.CallEvent(EventAction.ChangeController, this.controller, this);
        }

        private void DoReceivePause(bool pause)
        {
            GetLogicalComponent<InputReaderComponent>().Pause = pause;
        }
        #endregion
    }
}
