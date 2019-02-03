using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FMS
{
    public abstract class AIStateMachine : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        protected string current;
        #endregion

        #region Protected Methods
        protected StateMachine stateMachine;
        protected bool isActionFinish;
        protected Dictionary<string, IState> states;
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            stateMachine = new StateMachine();
            CreateDictonaryState();
        }

        private void Start()
        {
            ChangeState(current);
        }

        private void Update()
        {
            IsChangeState();
            isActionFinish = this.stateMachine.ExecuteStateUpdate();
        }
        #endregion

        #region Protected Methods
        protected void ChangeState(string state)
        {
            isActionFinish = false;
            current = state;
            this.stateMachine.ChangeState(states[current]);
        }

        protected abstract void IsChangeState();
        #endregion

        #region Abstract Methods
        protected abstract void CreateDictonaryState();
        #endregion
    }
}
