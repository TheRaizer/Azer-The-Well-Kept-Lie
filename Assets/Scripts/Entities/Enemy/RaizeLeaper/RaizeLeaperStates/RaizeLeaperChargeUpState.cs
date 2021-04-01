using Azer.GeneralEnemy;
using UnityEngine;

namespace Azer.States
{
    public class RaizeLeaperChargeUpState : State
    {
        private readonly RaizeLeaperController controller;
        private readonly Rigidbody2D rb;

        public RaizeLeaperChargeUpState(StateMachine _stateMachine, RaizeLeaperController _controller, Rigidbody2D _rb) : base(_stateMachine)
        {
            controller = _controller;
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();

            rb.velocity = Vector2.zero;
            controller.RaizeAnim.SetLeapStage(1);
            controller.LeaperPause.StartTimer(controller.ChargeTime);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            controller.LeaperPause.PauseTimer();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(!controller.LeaperPause.Pausing)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperLeapState));
            }
        }

        public override void Exit()
        {
            base.Exit();

            controller.LeaperPause.UnPause();
        }
    }
}
