using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pearl;
using Pearl.Events;
using System;

namespace Pearl.Demo
{
    public class ForceManagerCharacter : ForceManager
    {
        #region Events
        public event genericDelegate<Vector2, bool> OnCollision;
        #endregion


        #region Private Fields
        private enum Axis { Right, Up}
        private static readonly LayerMask mask;
        private RaycastHit2D[] hits;
        private List<RaycastHit2D> realHits;
        private const float distanceFromCollider = 0.01f;

        private readonly float distanceX;
        private readonly float distanceY;
        private readonly Vector2 sizeRectangleHitX;
        private readonly Vector2 sizeRectangleHitY;

        private Vector2 direction;
        #endregion

        #region Constructors
        static ForceManagerCharacter()
        {
            mask = LayerExtend.CreateLayerMask("Obstacle");
        }

        public ForceManagerCharacter(Rigidbody2D rigidBody, BoxCollider2D collider, genericDelegate<Vector2, bool> del) : this(rigidBody, collider, null, del)
        {
        }

        public ForceManagerCharacter(Rigidbody2D rigidBody, BoxCollider2D collider, Force gravity, genericDelegate<Vector2, bool> del) : base(rigidBody, collider, gravity)
        {
            distanceX = (collider.size.x * 0.5f) + distanceFromCollider;
            distanceY = (collider.size.x * 0.5f) + distanceFromCollider;
            sizeRectangleHitX = new Vector2(distanceFromCollider, collider.size.y);
            sizeRectangleHitY = new Vector2(collider.size.x, distanceFromCollider);
            realHits = new List<RaycastHit2D>();
            OnCollision = del;
        }
        #endregion

        #region Private Methods
        protected override void ControlObstacle()
        {
            if (forceTotal.x != 0)
            {
                direction = (Vector2.right * forceTotal.x).normalized;
                bool isCollision = IsThisObstacle(direction, distanceX, sizeRectangleHitX, Axis.Right);
                OnCollision?.Invoke(direction, isCollision);
                if (isCollision)
                    forceTotal.x = 0;
            }

            if (forceTotal.y != 0)
            {
                direction = (Vector2.up * forceTotal.y).normalized;
                bool isCollision = IsThisObstacle(direction, distanceY, sizeRectangleHitY, Axis.Up);
                OnCollision?.Invoke(direction, isCollision);
                if (isCollision)
                    forceTotal.y = 0;
            }
        }

        private bool IsThisObstacle(Vector2 direction, float distance, Vector2 sizeRectangleHit, Axis axis)
        {
            hits = Physics2D.BoxCastAll(rigidBody.position, sizeRectangleHit, 0, direction, distance, mask);
            realHits.Clear();
            foreach (RaycastHit2D hit in hits)
            {
                if (axis == Axis.Right && hit.normal.x != 0)
                    realHits.Add(hit);
                else if (axis == Axis.Up && hit.normal.y != 0)
                    realHits.Add(hit);
            }
            return realHits.Count > 0;
        }
        #endregion
    }
}
