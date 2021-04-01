using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.GeneralEnemy;

namespace Azer.States
{
    public class SlimeJumpAggroState : State
    {
        private readonly SlimeController slimeController;

        public SlimeJumpAggroState(StateMachine _stateMachine, SlimeController _slimeController) : base(_stateMachine)
        {
            slimeController = _slimeController;
        }

        public override void Enter()
        {
            base.Enter();
            slimeController.SlimeAnim.SetSlimeState(1);
            slimeController.JumpLogic.JumpAggro();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (slimeController.GroundCheck.IsGrounded)
            {
                stateMachine.ChangeState(typeof(SlimePausedState));
            }
        }
    }
}
