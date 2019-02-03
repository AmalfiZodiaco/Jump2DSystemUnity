using UnityEngine;
using System;
using Pearl.Events;
using Pearl;
using Pearl.InputSystem;
using Pearl.Clock;

namespace Pearl.Demo.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerManager : LogicalManager
    {
        #region Inspector Fields
        [Header("Movement Settings")]
        [Range(10, 20)]
        [SerializeField]
        [Tooltip("The speed of movement")]
        private float speed = 5;
        [SerializeField]
        [Tooltip("The direction of player")]
        private Vector2 direction = Vector2.right;
        [Header("Jump Settings")]
        [Range(0, 20)]
        [SerializeField]
        [Tooltip("The initial power of the jump")]
        private float forceJump = 1;
        [Range(0, 5)]
        [SerializeField]
        [Tooltip("The time needed to stop the power of the jump")]
        private float timeForceJump = 1;
        [SerializeField]
        [Range(0, 1)]
        [Tooltip("The percentage of movement that the player can perform in the air")]
        private float percentMovementInAir = 1;
        [SerializeField]
        [Tooltip("The force degeneration curve. On the x axis there is the time in percentage and in the axis of the y there is the power of the jump in percentage(for example if the curve is a straight line it means that the power is linear at the time)")]
        private AnimationCurve forceJumpDegenerateCurve;
        #endregion

        #region Properties
        public Vector2 Direction { get { return direction; } }
        #endregion

        #region Private Fields
        private PlayerMovementComponent mvComponent;
        #endregion

        #region Unity CallBacks
        protected override void Awake()
        {
            base.Awake();
            mvComponent = GetLogicalComponent<PlayerMovementComponent>();
            EventsManager.GetIstance<ForceManagerSystem>().AddManagerCharacterForce(gameObject.GetInstanceID(), 
                GetComponent<Rigidbody2D>(), GetComponent<BoxCollider2D>(), true,
                ReceiveControlCollider);
            SubscribeEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventsManager.RemoveMethod<genericDelegate<float>>(EventFastAction.GetInputMovement, ReceiveInputMovement);
        }

        public void OnValidate()
        {
            if (mvComponent != null)
            {
                mvComponent.PercentMovementInAir = percentMovementInAir;
                mvComponent.Speed = speed;
                mvComponent.ForceJump = forceJump;
                mvComponent.TimeForceJump = timeForceJump;
                mvComponent.ForceJumpDegenerateCurve = forceJumpDegenerateCurve;
            }
        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents.Add(typeof(PlayerMovementComponent), new PlayerMovementComponent(gameObject.GetInstanceID(), speed, 
                percentMovementInAir, forceJump, timeForceJump, forceJumpDegenerateCurve, 
                direction, GetComponent<BoxCollider2D>(), ReceiveDirection));
        }
        #endregion

        #region Interface Methods

        #region Add/Remove Methods in Events
        private void SubscribeEvents()
        {
            EventsManager.AddMethod<genericDelegate<float>>(EventFastAction.GetInputMovement, ReceiveInputMovement);
            EventsManager.AddMethod<genericDelegate>(EventAction.GetInputJump, ReceiveInputJump, this);
        }
        #endregion


        #region Receive Methods
        private void ReceiveInputMovement(float input)
        {
            DoMove(input);
        }

        private void ReceiveInputJump()
        {
            DoJump();
        }

        private void ReceiveDirection(Vector2 direction)
        {
            DoSetDirection(direction);
        }

        private void ReceiveControlCollider(Vector2 direction, bool isCollision)
        {
            mvComponent.ControlCollider(direction, isCollision);
        }
        #endregion

        #endregion

        #region Logical Methods
        private void DoMove(float input)
        {
            mvComponent.Move(input);
        }

        private void DoJump()
        {
            mvComponent.Jump();
        }

        private void DoSetDirection(Vector2 direction)
        {
            this.direction = direction;
        }
        #endregion
    }
}
