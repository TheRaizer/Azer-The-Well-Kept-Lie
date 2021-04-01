using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;
using Azer.States;
using System;
using Azer.Player;

namespace Azer.GeneralEnemy
{
    public class SlimeController : MonoBehaviour, IEnemy, IAggroRange, IChangeToKnockState
    {

        [field: SerializeField] public Enemy ParentEnemy { get; private set; }

        [Header("Checks")]
        [SerializeField] private BoxCollider2D boxCollider = null;
        [SerializeField] private Transform wallPoint = null;
        [SerializeField] private Transform edgePoint = null;
        [SerializeField] private float radius = 0.05f, distance = 20;

        [Header("Layers to Check")]
        [SerializeField] private LayerMask wall = 1 << 1;
        [SerializeField] private LayerMask wallAndMoveableObject = 1 << 1;

        [Header("Jump Forces")]
        [SerializeField] private float jumpForceX = 2;
        [SerializeField] private float jumpForceY = 6;

        [Header("Pause Times")]
        [SerializeField] private float pauseTimeMin = 1;
        [SerializeField] private float pauseTimeMax = 2;
        [field: SerializeField] public float AggroPause { get; private set; } = 0.3f;


        private HealthManagerTemplate health;
        private Rigidbody2D rb;
        private GravityMultiplier gravity;
        private EnemyKnockBack enemyKnock;
        private IJump jump;


        public SlimeAnimator SlimeAnim { get; private set; }
        public FlipSprite FlipSprite { get; private set; }
        public CheckEntityGrounded GroundCheck { get; private set; }
        public CheckEntityAtWall WallCheck { get; private set; }
        public CheckEntityAtEdge EdgeCheck { get; private set; }
        public EnemyHoppingMovement JumpLogic { get; private set; }
        public EnemyPauseLogic PauseLogic { get; private set; }


        [SerializeField] GameObject SlimeAnimatorRenderer = null;

        private StateMachine stateMachine;
        private readonly Dictionary<Type, State> states = new Dictionary<Type, State>();

        public bool ChangeDirection { get; set; }

        private void Awake()
        {
            FlipSprite = GetComponent<FlipSprite>();

            enemyKnock = GetComponent<EnemyKnockBack>();
            SlimeAnim = GetComponent<SlimeAnimator>();
            health = GetComponent<HealthManagerTemplate>();

            rb = GetComponent<Rigidbody2D>();

            gravity = new GravityMultiplier(rb, false, null);
            health.OnDeath = () => OnSlimeDeath();

            GroundCheck = new CheckEntityGrounded(rb, wall, boxCollider, radius);
            WallCheck = new CheckEntityAtWall(wallPoint, radius, wallAndMoveableObject);
            EdgeCheck = new CheckEntityAtEdge(edgePoint, distance, wall);

            jump = new EntityJump(rb)
            {
                JumpForceX = jumpForceX,
                JumpForceY = jumpForceY
            };

            JumpLogic = new EnemyHoppingMovement(rb, jump, GroundCheck, FlipSprite, FindObjectOfType<PlayerController>().transform, transform);
            PauseLogic = new EnemyPauseLogic(pauseTimeMin, pauseTimeMax);

            JumpLogic.SetJumpForceX(jumpForceX);

            stateMachine = new StateMachine();
            states.Add(typeof(SlimeJumpRoamState), new SlimeJumpRoamState(stateMachine, this));
            states.Add(typeof(SlimeJumpAggroState), new SlimeJumpAggroState(stateMachine, this));
            states.Add(typeof(DeadState), new DeadState(stateMachine, rb));
            states.Add(typeof(KnockBackState), new KnockBackState(stateMachine, enemyKnock, GroundCheck, typeof(SlimePausedState)));
            states.Add(typeof(SlimePausedState), new SlimePausedState(stateMachine, rb, this));
        }

        private void Start()
        {
            stateMachine.SetStates(states);
            stateMachine.Initialize(typeof(SlimeJumpRoamState));
        }

        private void Update()
        {
            stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            if (stateMachine.CurrentState.GetType() != typeof(DeadState))
            {
                gravity.MultiplyGravity();
            }

            stateMachine.CurrentState.PhysicsUpdate();

            if(SlimeAnimatorRenderer.activeSelf == false && stateMachine.CurrentState.GetType() != typeof(DeadState))
            {
                stateMachine.ChangeState(typeof(DeadState));
            }
        }

        private void LateUpdate()
        {
            if (stateMachine.CurrentState.GetType() != typeof(DeadState))
            {
                GroundCheck.GroundCheck();
                WallCheck.WallCheck(transform);
                EdgeCheck.EdgeCheck();
            }

            stateMachine.CurrentState.HandleInput();
        }

        private void OnSlimeDeath()
        {
            stateMachine.ChangeState(typeof(DeadState));
            SlimeAnimatorRenderer.SetActive(false);

            rb.bodyType = RigidbodyType2D.Static;
        }

        public void OnEnter()
        {
            ParentEnemy.InAggro = true;
        }

        public void OnExit()
        {
            ParentEnemy.InAggro = false;
        }

        public void ChangeToKnockBackState()
        {
            stateMachine.ChangeState(typeof(KnockBackState));
        }
    }
}
