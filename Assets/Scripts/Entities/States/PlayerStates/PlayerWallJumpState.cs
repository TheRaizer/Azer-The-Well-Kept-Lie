using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.Player;
using Azer.EntityComponents;

namespace Azer.States
{
    public class PlayerWallJumpState : State
    {
        private readonly PlayerController player;
        private readonly PlayerWallJumpLogic wallActions;

        private IEnumerator wallJumpCo;

        public PlayerWallJumpState(StateMachine _stateMachine, PlayerController _player, PlayerWallJumpLogic _wallActions, IJump _jump) : base(_stateMachine)
        {
            player = _player;
            wallActions = _wallActions;

            wallActions.SetJump(_jump);
        }

        public override void Enter()
        {
            base.Enter();

            player.PlayerAnim.SetJumpParam(true);

            wallJumpCo = wallActions.WallJumpCo();

            player.StartCoroutine(wallJumpCo);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.FlipSprite.FlipTransformScale(player.PlayerValues.Horiz);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (!wallActions.WallJumped)
            {
                stateMachine.ChangeState(typeof(PlayerFallingState));
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) && player.DashCount == 0)
            {
                stateMachine.ChangeState(typeof(PlayerDashState));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            wallActions.LerpWhenWallJumped();
        }

        public override void Exit()
        {
            base.Exit();
            player.PlayerAnim.SetJumpParam(false);
            player.StopCoroutine(wallJumpCo);
            wallActions.EndJump();
        }

    }
}
