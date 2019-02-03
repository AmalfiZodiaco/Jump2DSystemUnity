using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.DecisionTree
{
    public class EnemyAI : DecisionTree
    {
        #region Inspector Fields
        [SerializeField]
        private Transform destination;
        #endregion

        #region Override Methods
        protected override void CreateDecisionTree()
        {
            root = new Node(2)
            {
                Decide = delegate ()
                {
                    if ((destination.position - transform.position).magnitude > 10)
                        return 1;
                    else
                        return 2;
                }
            };

            
            //root.SetChild(1, new PathFinding(destination, GetComponent<NavMeshAgent>()));
            //root.SetChild(2, new Talk());
        }
        #endregion
    }
}
