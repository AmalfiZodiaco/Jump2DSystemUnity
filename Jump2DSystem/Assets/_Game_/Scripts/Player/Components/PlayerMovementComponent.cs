using System.Collections.Generic;
using UnityEngine;
using Pearl.Events;
using Pearl;
using System;

namespace Pearl.Demo.Player
{
    public class PlayerMovementComponent : LogicalComponent
    {
        #region Private Fields
        private float forceJump;
        private float speed;
        private float timeForceJump;
        private AnimationCurve forceJumpDegenerateCurve;
        private readonly int id;
        private readonly BoxCollider2D boxCollider;
        private readonly Action<Vector2> notifyDirection;
        private const string nameMovementForce = "movement";
        private const string nameJumpForce = "jump";
        private bool isGrounded;
        private float percentMovementInAir;
        private ForceManagerSystem forceManager;
        private Vector2 auxVector;
        #endregion

        #region Propieties
        public bool IsGrounded { set { isGrounded = value; } }
        public float Speed { set { speed = value; } }
        public float TimeForceJump { set { timeForceJump = value; } }
        public float ForceJump { set { forceJump = value - forceManager.Gravity.y; } }
        public float PercentMovementInAir { set { percentMovementInAir = value; } }
        public AnimationCurve ForceJumpDegenerateCurve { set { forceJumpDegenerateCurve = value; } }
        #endregion

        #region Constrctors
        public PlayerMovementComponent(int ID, float speed, float percentMovementInAir,
            float speedJump, float timeForceJump, AnimationCurve forceJumpDegenerateCurve,
            Vector2 direction, BoxCollider2D boxCollider, Action<Vector2> notifydirection)
        {
            forceManager = EventsManager.GetIstance<ForceManagerSystem>();
            this.boxCollider = boxCollider;
            this.forceJump = speedJump - forceManager.Gravity.y;
            this.timeForceJump = timeForceJump;
            this.forceJumpDegenerateCurve = forceJumpDegenerateCurve;
            this.speed = speed;
            this.auxVector = direction;
            this.id = ID;
            this.notifyDirection = notifydirection;
            this.percentMovementInAir = percentMovementInAir;

            notifyDirection.Invoke(direction.normalized);
        }
        #endregion

        #region Slave Methods
        public void ControlCollider(Vector2 direction, bool isCollision)
        {
            if (direction.y > 0)
                isGrounded = false;
            if (direction.y < 0)
                isGrounded = isCollision;
        }

        public void Move(float valueInput)
        {
            auxVector = Vector2.right * valueInput * speed;
            if (valueInput != 0)
                notifyDirection.Invoke(auxVector.normalized);

            if (!isGrounded)
                auxVector *= percentMovementInAir;

            forceManager.AddForce(id, nameMovementForce, auxVector);
        }

        public void Jump()
        {
            if (isGrounded)
            {
                EventsManager.GetIstance<ForceManagerSystem>().AddForce(id, "jump",
                    Vector2.up * forceJump, timeForceJump, null, forceJumpDegenerateCurve);
            }
        }
        #endregion
    }
}
