using Azer.GeneralEnemy;

namespace Azer.States
{
    public class RaizeLeaperRoamState : State
    {
        private readonly RaizeLeaperController controller;

        public RaizeLeaperRoamState(StateMachine _stateMachine, RaizeLeaperController _controller) : base(_stateMachine)
        {
            controller = _controller;
        }

        public override void Enter()
        {
            base.Enter();

            controller.RaizeAnim.SetSpeed(1);
            controller.LeaperRoam.GeneratePoint();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            controller.LeaperRoam.Movement();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(controller.LeaperRoam.AtPoint)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperPauseTickState));
            }
            if(controller.ParentEnemy.InAggro)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperAggro));
            }
        }

        public override void Exit()
        {
            base.Exit();

            controller.LeaperRoam.AtPoint = false;
        }
    }
}
