using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System;

namespace AI.DecisionTree
{
    public class Node : ICloneable
    {
        #region Public Fields
        public delegate int OnCondition();
        public OnCondition Decide;
        #endregion

        #region Private Fields
        private readonly Node[] childList;
        #endregion

        #region Constructors
        public Node(int countChild)
        {
            childList = new Node[countChild];
        }
        #endregion

        #region Public Fields
        public IState ChooseAction()
        {
            int child = Decide() - 1;
            if (typeof(IState).IsAssignableFrom(childList[child].GetType()))
                return (IState)childList[child];
            else
                return childList[child].ChooseAction();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void SetChild(int child, Node content)
        {
            childList[child - 1] = content;
        }
        #endregion
    }
}

