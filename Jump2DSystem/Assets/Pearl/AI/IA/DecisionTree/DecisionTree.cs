using UnityEngine;

namespace AI.DecisionTree
{
    public abstract class DecisionTree : MonoBehaviour
    {
        #region Protected Fields
        protected Node root;
        #endregion

        #region Private Fields
        private IState currentAction;
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            CreateDecisionTree();
        }

        private void Update()
        {
            IState aux = root.ChooseAction();
            if (aux != currentAction)
                currentAction?.Exit();
            currentAction = root.ChooseAction();
            currentAction.Execute();
        }
        #endregion

        #region Abstract Methods
        protected abstract void CreateDecisionTree();
        #endregion
    }
}