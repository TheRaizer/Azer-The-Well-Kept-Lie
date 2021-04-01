using Azer.EntityComponents;
using Azer.GeneralEnemy;

namespace Azer.States
{
    public class RaizeLeaperAggro : State
    {
        private readonly RaizeLeaperController controller;
        private readonly float aggroMoveSpeed;

        public RaizeLeaperAggro(StateMachine _stateMachine, RaizeLeaperController _controller, float _aggroMoveSpeed) : base(_stateMachine)
        {
            controller = _controller;
            aggroMoveSpeed = _aggroMoveSpeed;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            controller.RaizeAnim.SetSpeed(1);
            controller.MoveToPlayer.MoveToEntity(aggroMoveSpeed);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(!controller.ParentEnemy.InAggro)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperRoamState));
            }
            if(controller.AttackRange.InAttackRange)
            {
                stateMachine.ChangeState(typeof(RaizeLeaperChargeUpState));
            }
        }
    }
}
