using Pearl.Events;
using UnityEngine;
using System;

namespace Pearl.InputSystem
{
    public class InputReaderComponent : InputReaderAbstractComponent
    {
        #region Public Methods
        public override void DoUpdateKeyboard()
        {
            if (Input.GetButtonDown("Submit"))
                EventsManager.CallEvent(EventFastAction.GetInputEntryMenu);
            if (Input.GetButtonDown("Cancel"))
                EventsManager.CallEvent(EventFastAction.GetInputReturnUI);

            if (!isPause)
            {
                EventsManager.CallEvent(EventFastAction.GetInputMovement, Input.GetAxisRaw("Horizontal"));

                if (Input.GetButtonDown("Jump"))
                    EventsManager.CallEvent(EventAction.GetInputJump);

                if (Input.GetButtonDown("ComandOldMan"))
                    EventsManager.CallEvent(EventAction.GetInputComandOldMan, EventAction.GetInputComandOldMan);

                if (Input.GetButtonDown("ComandBoy"))
                    EventsManager.CallEvent(EventAction.GetInputComandOldMan, EventAction.GetInputComandOldMan);
            }
        }


        public override void DoUpdateJoystick()
        {
        }
        #endregion
    }
}
