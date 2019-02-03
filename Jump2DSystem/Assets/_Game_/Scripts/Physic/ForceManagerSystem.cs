using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pearl;
using Pearl.Events;
using System;
using Pearl.Demo.Player;

namespace Pearl.Demo
{
    public class ForceManagerSystem : LogicalManager, ISingleton
    {
        #region Insepctor Fields
        [SerializeField]
        [Tooltip("The gravity of the game")]
        private Vector2 gravity;
        #endregion

        #region Private Fields
        public enum TypeForce { instantly, degenerate }
        public enum InitForce { enable, disable}
        private const string gravityString = "gravity";
        private Dictionary<int, ForceManager> forcesManager = new Dictionary<int, ForceManager>();
        private Dictionary<int, ForceManager> forcesManagerDisable = new Dictionary<int, ForceManager>();
        #endregion

        #region Properties
        public Vector2 Gravity { get { return gravity; } }
        #endregion

        #region Unity Callbacks
        // Update is called once per frame
        private void FixedUpdate()
        {
            ManageForces();
        }

        private void OnValidate()
        {
            foreach (ForceManager forceManager in forcesManager.Values)
            {
                if (forceManager.Contains(gravityString))
                    forceManager.Add(gravityString, new Force(gravity, gravityString));
            }
            GameObject.FindObjectOfType<PlayerManager>().OnValidate();
        }
        #endregion

        #region Public Methods
        public void DisableForce(int ID)
        {
            forcesManagerDisable.Add(ID, forcesManager[ID]);
            forcesManager.Remove(ID);
        }

        public void EnableForce(int ID)
        {
            forcesManager.Add(ID, forcesManagerDisable[ID]);
            forcesManagerDisable.Remove(ID);
        }

        public void AddManagerCharacterForce(int ID, Rigidbody2D rigidBody, BoxCollider2D boxCollider, bool isGravity, genericDelegate<Vector2, bool> del)
        {
            if (isGravity)
                forcesManager.Add(ID, new ForceManagerCharacter(rigidBody, boxCollider, new Force(gravity, gravityString), del));
            else
                forcesManager.Add(ID, new ForceManagerCharacter(rigidBody, boxCollider, del));
        }

        public void AddManagerForce(int ID, Rigidbody2D rigidBody, BoxCollider2D boxCollider, bool isGravity, InitForce init = InitForce.enable)
        {
            if (isGravity)
                forcesManager.Add(ID, new ForceManager(rigidBody, boxCollider, new Force(gravity, gravityString)));
            else
                forcesManager.Add(ID, new ForceManager(rigidBody, boxCollider));


            if (init == InitForce.disable)
                DisableForce(ID);
        }

        public void RemoveManagerForce(int ID)
        {
            forcesManager.Remove(ID);
        }

        public void RemoveForceContinue(int ID, string keyForceContinue)
        {
            forcesManager[ID].RemoveForceContinue(keyForceContinue);
        }

        public void AddForce(int ID, string keyForceContinue, Vector3 force)
        {
            forcesManager[ID].Add(keyForceContinue, new Force(force, name));
        }

        public void AddForce(int ID, string keyForce, Vector3 force, float timer, TypeForce type)
        {
            if (type == TypeForce.instantly)
                forcesManager[ID].Add(keyForce, new ForceTimer(force, name, timer));
            else
                forcesManager[ID].Add(keyForce, new ForceDegenerateTimer(force, name, timer));
        }

        public void AddForce(int ID, string keyForce, Vector3 force, float timer, AnimationCurve curveX, AnimationCurve curveY)
        {
            forcesManager[ID].Add(keyForce, new ForceDegenerateCurveTimer(force, name, timer, curveX, curveY));
        }
        #endregion

        #region Private Methods
        private void ManageForces()
        {
            foreach (ForceManager forceManager in forcesManager.Values)
            {
                forceManager.AddTotalForce();
            }
        }
        #endregion
    }
}
