using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FMS
{
    public class EnemyAI : AIStateMachine
    {
        #region Inspector Fields
        [SerializeField]
        private Transform destination;
        #endregion

        #region Override Methods
        protected override void CreateDictonaryState()
        {
            states = new Dictionary<string, IState>
        {
            { "Search", new PathFinding(destination, GetComponent<NavMeshAgent>()) },
            { "Dialog", new Talk() }
        };
        }

        protected override void IsChangeState()
        {
            switch (current)
            {
                case "Search":
                    if (isActionFinish)
                        ChangeState("Dialog");
                    break;
                case "Dialog":
                    break;
            }
        }
        #endregion
    }

}