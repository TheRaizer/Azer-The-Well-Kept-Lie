using Azer.GeneralEnemy;
using UnityEngine;

namespace Azer.States
{
    public class RaizeLeaperPauseTickState : State
    {
        private readonly RaizeLeaperController controller;
        private readonly Rigidbody2D rb;

        private readonly System.Random rnd = new System.Random();

        public RaizeLeaperPauseTickState(StateMachine _stateMachine, RaizeLeaperController _controller, Rigidbody2D _rb) : base(_stateMachine)
        {
            controller = _controller;
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();

            controller.RaizeAnim.SetSpeed(-1);
            rb.velocity = Vector2.zero;


            int tickCount = Mathf.RoundToInt(rnd.Next(0, 3));
            controller.RaizeAnim.SetIdleTick(tickCount);
            controller.EndTick.Tick = true;

            controller.LeaperPause.StartTimer(0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            controller.LeaperPause.PauseTimer();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(!controller.LeaperPause.Pausing && !controller.ParentEnemy.InAggro)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperRoamState));
            }
            if (controller.ParentEnemy.InAggro)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperAggro));
            }
            if (!controller.EndTick.Tick)
            {
                controller.RaizeAnim.SetIdleTick(0);
            }
        }

        public override void Exit()
        {
            base.Exit();
            controller.EndTick.Tick = false;
            controller.LeaperPause.UnPause();
        }
    }
}
