namespace AI.FMS
{
    public class StateMachine
    {
        #region Private Fields
        private IState currentlyRunningState;
        private IState previousState;
        #endregion

        #region Public Methods
        public void ChangeState(IState newState)
        {
            this.currentlyRunningState?.Exit();
            this.previousState = this.currentlyRunningState;
            this.currentlyRunningState = newState;
            this.currentlyRunningState.Enter();
        }

        public bool ExecuteStateUpdate()
        {
            IState runningState = this.currentlyRunningState;
            return runningState.Execute();
        }

        public void SwitchToPreviousState()
        {
            this.currentlyRunningState.Exit();
            this.currentlyRunningState = this.previousState;
            this.currentlyRunningState.Enter();
        }
        #endregion
    }
}
