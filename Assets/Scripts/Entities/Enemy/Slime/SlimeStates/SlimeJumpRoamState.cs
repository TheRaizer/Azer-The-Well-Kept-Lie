using Azer.GeneralEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.States
{
    public class SlimeJumpRoamState : State
    {
        private readonly SlimeController slimeController;

        public SlimeJumpRoamState(StateMachine _stateMachine, SlimeController _slimeController) : base(_stateMachine)
        {
            slimeController = _slimeController;
        }

        public override void Enter()
        {
            base.Enter();
            slimeController.SlimeAnim.SetSlimeState(1);
            slimeController.JumpLogic.JumpRoam();
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
