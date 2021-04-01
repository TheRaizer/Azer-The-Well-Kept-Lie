using System.Collections.Generic;
using UnityEngine;
using Azer.States;
using Assets.Scripts.Player;
using Azer.EntityComponents;
using System;

namespace Azer.Player
{
    public class PlayerController : MonoBehaviour, IChangeToKnockState
    {

        [SerializeField] private BoxCollider2D boxCollider = null;
        [SerializeField] private Transform wallPoint = null;

        [field: SerializeField] public int DashCount { get; set; }

        [field: SerializeField] public PlayerValues PlayerValues { get; private set; }

        public float JumpBufferTimer { get; private set; } = 0f;
        public float CoyoteTimer { get; private set; } = 0f;

        public EntityPush Dash { get; private set; }
        public CheckEntityAtWall WallCheck { get; private set; }
        public CheckEntityGrounded GroundCheck { get; private set; }
        public PlayerAnimator PlayerAnim { get; private set; }
        public FlipSprite FlipSprite { get; private set; }

        private IJump jump;
        private PlayerWallJumpLogic wallActions;
        private PlayerHealthManager health;
        private Rigidbody2D rb;
        private PlayerSwing swing;
        private GravityMultiplier gravity;
        private PlayerRespawn playerRespawn;
        private PlayerKnockBackLogic knockBack;


        private StateMachine stateMachine;
        private readonly Dictionary<Type, State> states = new Dictionary<Type, State>();

        public bool CameFromWall { get; set; }//ugly but works for now

        private void Awake()
        {
            knockBack = GetComponent<PlayerKnockBackLogic>();
            wallActions = GetComponent<PlayerWallJumpLogic>();
            playerRespawn = FindObjectOfType<PlayerRespawn>();
            swing = GetComponent<PlayerSwing>();
            PlayerAnim = GetComponent<PlayerAnimator>();
            rb = GetComponent<Rigidbody2D>();
            FlipSprite = GetComponent<FlipSprite>();
            health = GetComponent<PlayerHealthManager>();

            jump = new EntityJump(rb);

            Dash = new EntityPush(rb);

            WallCheck = new CheckEntityAtWall(wallPoint, PlayerValues.WallPointRadius, PlayerValues.Wall);

            GroundCheck = new CheckEntityGrounded(rb, PlayerValues.Floor, boxCollider, PlayerValues.GroundPointRadius)
            {
                OnGrounded = () => PlayerAnim.SetIsGroundedParam(true),
                WhenNotGrounded = () => PlayerAnim.SetIsGroundedParam(false)
            };


            gravity = new GravityMultiplier(rb, true, Dash);
            health.OnDeath = () => playerRespawn.RespawnPlayer();


            stateMachine = new StateMachine();

            states.Add(typeof(PlayerIdleState), new PlayerIdleState(stateMachine, this, rb));
            states.Add(typeof(PlayerSwingState), new PlayerSwingState(stateMachine, this, swing, rb));
            states.Add(typeof(PlayerRunState), new PlayerRunState(stateMachine, this, rb));
            states.Add(typeof(PlayerJumpState), new PlayerJumpState(stateMachine, this, jump, rb));
            states.Add(typeof(PlayerDashState), new PlayerDashState(stateMachine, this, rb));
            states.Add(typeof(PlayerWallSlideState), new PlayerWallSlideState(stateMachine, this, rb));
            states.Add(typeof(PlayerFallingState), new PlayerFallingState(stateMachine, this, rb));
            states.Add(typeof(PlayerWallJumpState), new PlayerWallJumpState(stateMachine, this, wallActions, jump));
            states.Add(typeof(PlayerKnockBackState), new PlayerKnockBackState(stateMachine, this, knockBack, rb));
        }

        void Start()
        {
            stateMachine.SetStates(states);

            stateMachine.Initialize(typeof(PlayerIdleState));
        }

        void Update()
        {
            if (GroundCheck.IsGrounded && DashCount != 0)
            {
                DashCount = 0;
            }

            stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            gravity.MultiplyGravity();

            stateMachine.CurrentState.PhysicsUpdate();
        }

        private void LateUpdate()
        {
            GroundCheck.GroundCheck();
            WallCheck.WallCheck(transform);
            
            HandleTimers();
            AxisCheck();

            stateMachine.CurrentState.HandleInput();
        }

        private void HandleTimers()
        {
            if (GroundCheck.IsGrounded || stateMachine.CurrentState.GetType() == typeof(PlayerWallSlideState))
            {
                CoyoteTimer = PlayerValues.CoyoteTime;
            }
            else
            {
                CoyoteTimer -= Time.deltaTime;
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                JumpBufferTimer = PlayerValues.JumpBufferTime;
            }
            else if (JumpBufferTimer >= 0)
            {
                JumpBufferTimer -= Time.deltaTime;
            }
        }

        private void AxisCheck()
        {
            PlayerValues.Horiz = Input.GetAxisRaw("Horizontal");
            PlayerValues.Vert = Input.GetAxisRaw("Vertical");

        }

        public void ChangeToKnockBackState()
        {
            stateMachine.ChangeState(typeof(PlayerKnockBackState));
        }

        public void ResetCoyoteTimer() => CoyoteTimer = PlayerValues.CoyoteTime;
        public void SetCoyoteTimerToZero() => CoyoteTimer = 0f;
        public void ResetJumpBufferTimer() => JumpBufferTimer = PlayerValues.JumpBufferTime;
        public void SetJumpBufferTimerToZero() => JumpBufferTimer = 0f;
        public Type GetPlayerCurrentStateType() => stateMachine.CurrentState.GetType();
    }
}
