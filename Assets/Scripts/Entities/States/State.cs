
namespace Azer.States
{
    public abstract class State
    {
        protected StateMachine stateMachine;

        protected State(StateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Exit()
        {

        }
    }
}
