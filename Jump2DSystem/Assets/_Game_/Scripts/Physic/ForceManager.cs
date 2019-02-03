using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pearl;

namespace Pearl.Demo
{
    public class ForceManager
    {
        #region Private Fields
        private static readonly float scaleFixedUpdate = 1f;
        private Dictionary<string, Force> forces;
        protected Vector2 forceTotal;
        protected Rigidbody2D rigidBody;
        protected BoxCollider2D collider;
        #endregion

        #region Constructors
        public ForceManager(Rigidbody2D rigidBody, BoxCollider2D collider) : this(rigidBody, collider, null)
        {
        }

        public ForceManager(Rigidbody2D rigidBody, BoxCollider2D collider, Force gravity)
        {
            forces = new Dictionary<string, Force>();
            if (gravity != null)
                Add("gravity", gravity);
            this.rigidBody = rigidBody;
            this.collider = collider;
        }
        #endregion

        #region Public Region
        public void AddTotalForce()
        {
            if (forces.Count != 0)
                AddTotalForce(forces.Values, scaleFixedUpdate, Time.fixedDeltaTime);
        }

        public void RemoveForceContinue(string keyForceContinue)
        {
            forces.Remove(keyForceContinue);
        }

        public void Add(string keyForce, Force force)
        {
            forces.Update(keyForce, force);
        }

        public bool Contains(string keyForce)
        {
            return forces.ContainsKey(keyForce);
        }
        #endregion

        #region Private Methods
        private void AddTotalForce(IEnumerable enumerable, float scale, float deltaTime)
        {
            forceTotal = Vector2.zero;
            foreach (Force force in enumerable)
            {
                if (!force.IsTerminated())
                    forceTotal += force.GetForce();
                else
                    forces.Remove(force.Name);
            }

            forceTotal *= scale * deltaTime;
            ControlObstacle();
            Positioning(rigidBody.position + forceTotal);
        }

        protected virtual void ControlObstacle()
        {
        }

        private void Positioning(Vector2 newPosition)
        {
            rigidBody.MovePosition(newPosition);
        }
        #endregion
    }
}

