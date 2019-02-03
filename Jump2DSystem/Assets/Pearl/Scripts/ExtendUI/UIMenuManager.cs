using System.Collections.Generic;
using UnityEngine;
using Pearl.Events;
using System;
using Pearl.Game;
using System.Reflection;

namespace Pearl.UI
{
    /// <summary>
    /// The abstract class of the UI.This class must be the parent of every UI of the menu or pause.
    /// </summary>
    public class UIMenuManager : LogicalManager
    {
        #region Inspector Fields
        /// <summary>
        /// The first activated element of the UI
        /// </summary>
        [SerializeField]
        protected GameObject firstUIObjectEnable;
        /// <summary>
        /// The UI is the pre-game one or the one during the game
        /// </summary>
        [SerializeField]
        private StateUI stateUI = StateUI.Menu;
        #endregion

        #region Protected Fields
        protected bool isOpenUI = false;
        protected VisibilityPanelComponent visibilityComponent;
        protected SelectionPanelComponent selectionComponent;
        #endregion

        #region Private Fields
        private enum StateUI { Menu, Pause }
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        private void Start()
        {
            visibilityComponent = GetLogicalComponent<VisibilityPanelComponent>();
            selectionComponent = GetLogicalComponent<SelectionPanelComponent>();
            if (stateUI == StateUI.Menu)
            {
                isOpenUI = true;
                DoChangePanel(firstUIObjectEnable);
            }

            SubscribeEvents();
            InitOpenMenu();
        }

        protected override void OnDestroy()
        {
            base.Awake();
            RemoveEvents();
        }
        #endregion

        #region Init Methods
        protected virtual void InitOpenMenu()
        {
        }

        protected override void CreateComponents()
        {
            listComponents.Add(typeof(VisibilityPanelComponent), new VisibilityPanelComponent());
            listComponents.Add(typeof(SelectionPanelComponent), new SelectionPanelComponent());
        }
        #endregion

        #region Interface Methods

        #region UI interface Methods
        /// <summary>
        /// Calls The new Game
        /// </summary>
        public void UIStartNewGame(string scene)
        {
            DoStartNewGame(scene);
        }

        /// <summary>
        ///  This method must be called whenever you want to change the active button(and therefore often also panel)
        /// </summary>
        public void UIChangeActiveGameObject(GameObject obj)
        {
            DoChangeActiveGameObject(obj);
        }

        /// <summary>
        /// Calls The quit game
        /// </summary>
        public void UIQuit()
        {
            DoQuit();
        }
        #endregion

        #region Add/Remove Methods in Events
        protected void SubscribeEvents()
        {
            EventsManager.AddMethod<genericDelegate<bool>>(EventAction.CallPause, ReceivePause, this);

            EventsManager.AddMethod<genericDelegate>(EventFastAction.GetInputEntryMenu, ReceiveInputOpenCloseMenu);
            EventsManager.AddMethod<genericDelegate>(EventFastAction.GetInputReturnUI, ReceiveInputReturn);

        }

        protected void RemoveEvents()
        {
            EventsManager.RemoveMethod<genericDelegate>(EventFastAction.GetInputEntryMenu, ReceiveInputOpenCloseMenu);
            EventsManager.RemoveMethod<genericDelegate>(EventFastAction.GetInputReturnUI, ReceiveInputReturn);
        }
        #endregion

        #region Receive Methods
        private void ReceivePause(bool pause)
        {
            DoReceivePause(pause);
        }

        private void ReceiveInputOpenCloseMenu()
        {
            DoOpenCloseMenu();
        }

        private void ReceiveInputReturn()
        {
            DoReturn();
        }
        #endregion

        #region Send Methods
        private void SendCallPause()
        {
            EventsManager.CallEvent(EventAction.CallPause, !this.isOpenUI);
        }
        #endregion

        #endregion

        #region Logical Methods
        private void DoStartNewGame(string scene)
        {
            gameObject.SetActive(false);
            EventsManager.GetIstance<GameManager>().IEnterNewLevel(scene);
        }

        private void DoChangeActiveGameObject(GameObject obj)
        {
            if (visibilityComponent.ObeyIsSamePanel(obj))
                DoChangeButton(obj);
            else
                DoChangePanel(obj);
        }

        private void DoQuit()
        {
            #if UNITY_STANDALONE
                Application.Quit();
            #endif

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        private void DoReceivePause(bool pause)
        {
            this.isOpenUI = pause;
            if (!pause)
                DoCloseMenu();
            else
                DoChangePanel(firstUIObjectEnable);
        }

        private void DoChangePanel(GameObject obj)
        {
            visibilityComponent.ObeyShow(obj);
            selectionComponent.ObeyChangeSelectNext(obj);
        }

        private void DoCloseMenu()
        {
            visibilityComponent.ObeyOpenOrCloseAllPanels(false, transform);
            selectionComponent.ObeyReset();
        }

        private void DoChangeButton(GameObject obj)
        {
            selectionComponent.ObeyChangeSelectNext(obj);
        }

        private void DoOpenCloseMenu()
        {
            if (stateUI == StateUI.Pause)
                SendCallPause();
        }

        private void DoReturn()
        {
            if (isOpenUI)
            {
                if (!selectionComponent.IsOpenPage)
                {
                    visibilityComponent.ObeyShow(GetLogicalComponent<SelectionPanelComponent>().LastSelectedButton);
                    selectionComponent.ObeyChangeInPreSelect();
                }
                else if (stateUI == StateUI.Pause)
                    SendCallPause();
            }
        }
        #endregion
    }
}

