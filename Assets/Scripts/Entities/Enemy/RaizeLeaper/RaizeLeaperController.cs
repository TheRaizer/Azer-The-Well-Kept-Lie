using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;
using Azer.States;
using System;
using Azer.Player;

namespace Azer.GeneralEnemy
{
    public class RaizeLeaperController : MonoBehaviour, IEnemy, IAggroRange, IChangeToKnockState
    {
        #region Serialized Fields

        [SerializeField] private GameObject animatorRenderer = null;

        [Header("Movement Fields")]
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float aggroMoveSpeed = 2;
        [SerializeField] private Transform leftPoint = null,
                                           rightPoint = null;
        [SerializeField] private float minPause = 1.5f,
                                       maxPause = 2.5f;
        [field: SerializeField] public float ChargeTime { get; private set; } = 1f;

        [Header("Ground Fields")]
        [SerializeField] private LayerMask floor = 1 << 1;
        [SerializeField] private BoxCollider2D hitBox = null;
        [SerializeField] private float groundCheckDistance = 0.05f;

        [field: Header("Leap Properties")]
        [field: SerializeField] public float LeapTime { get; private set; }
        [field: SerializeField] public float LeapAmt { get; private set; }

        [field: SerializeField] public Enemy ParentEnemy { get; private set; }
        #endregion


        private HealthManagerTemplate health;
        private Rigidbody2D rb;
        private GravityMultiplier gravity;
        private EnemyKnockBack enemyKnock;
        private FlipSprite flipSprite;
        private CheckEntityGrounded groundCheck;
        private EntityPush leap;

        public RaizeEndTick EndTick { get; private set; }
        public RaizeLeaperAnimator RaizeAnim { get; private set; }
        public EnemyWalkBetweenTwoPoints LeaperRoam { get; private set; }
        public EnemyPauseLogic LeaperPause { get; private set; }
        public MoveToEntityVelocity MoveToPlayer { get; private set; }
        public AttackRange AttackRange { get; private set; }


        private StateMachine stateMachine;
        private readonly Dictionary<Type, State> states = new Dictionary<Type, State>();

        public void Awake()
        {
            AttackRange = GetComponent<AttackRange>();
            EndTick = GetComponentInChildren<RaizeEndTick>();
            RaizeAnim = GetComponent<RaizeLeaperAnimator>();
            flipSprite = GetComponent<FlipSprite>();
            enemyKnock = GetComponent<EnemyKnockBack>();
            health = GetComponent<HealthManagerTemplate>();
            rb = GetComponent<Rigidbody2D>();

            health.OnDeath = () => OnDeath();

            groundCheck = new CheckEntityGrounded(rb, floor, hitBox, groundCheckDistance);

            LeaperRoam = new EnemyWalkBetweenTwoPoints(transform, flipSprite, rb, moveSpeed);
            LeaperRoam.SetLeftRightPoints(leftPoint.position, rightPoint.position);

            LeaperPause = new EnemyPauseLogic(minPause, maxPause);

            gravity = new GravityMultiplier(rb, false, null);
            MoveToPlayer = new MoveToEntityVelocity(rb, FindObjectOfType<PlayerController>().transform, false, flipSprite);

            leap = new EntityPush(rb);

            stateMachine = new StateMachine();
            states.Add(typeof(RaizeLeaperRoamState), new RaizeLeaperRoamState(stateMachine, this));
            states.Add(typeof(RaizeLeaperPauseTickState), new RaizeLeaperPauseTickState(stateMachine, this, rb));
            states.Add(typeof(RaizeLeaperAggro), new RaizeLeaperAggro(stateMachine, this, aggroMoveSpeed));
            states.Add(typeof(RaizeLeaperChargeUpState), new RaizeLeaperChargeUpState(stateMachine, this, rb));
            states.Add(typeof(RaizeLeaperLeapState), new RaizeLeaperLeapState(stateMachine, this, leap, flipSprite));
            states.Add(typeof(KnockBackState), new KnockBackState(stateMachine, enemyKnock, groundCheck, typeof(RaizeLeaperAggro)));
            states.Add(typeof(DeadState), new DeadState(stateMachine, rb));
        }

        void Start()
        {
            stateMachine.SetStates(states);
            stateMachine.Initialize(typeof(RaizeLeaperRoamState));
        }

        void Update()
        {
            stateMachine.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            if (stateMachine.CurrentState.GetType() != typeof(DeadState))
            {
                gravity.MultiplyGravity();
                AttackRange.CheckIfInRange();
            }
            stateMachine.CurrentState.PhysicsUpdate();
        }

        void LateUpdate()
        {
            if (stateMachine.CurrentState.GetType() != typeof(DeadState))
            {
                groundCheck.GroundCheck();
            }

            stateMachine.CurrentState.HandleInput();
        }

        private void OnDeath()
        {
            stateMachine.ChangeState(typeof(DeadState));
            animatorRenderer.SetActive(false);

            rb.bodyType = RigidbodyType2D.Static;
        }

        public void ChangeToKnockBackState()
        {
            stateMachine.ChangeState(typeof(KnockBackState));
        }

        public void OnEnter()
        {
            ParentEnemy.InAggro = true;
        }

        public void OnExit()
        {
            ParentEnemy.InAggro = false;
        }
    }
}
