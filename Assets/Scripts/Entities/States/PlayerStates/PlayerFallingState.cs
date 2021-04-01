using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.Player;

namespace Azer.States
{
    public class PlayerFallingState : PlayerMoveableState
    {
        private readonly PlayerController player;

        public PlayerFallingState(StateMachine _stateMachine, PlayerController _player, Rigidbody2D _rb) : base(_stateMachine, _player, _rb)
        {
            player = _player;
        }

        public override void Enter()
        {
            base.Enter();

            player.PlayerAnim.SetFallingParam(true);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (player.GroundCheck.IsGrounded && player.PlayerValues.Horiz == 0)
            {
                stateMachine.ChangeState(typeof(PlayerIdleState));
            }
            else if (player.GroundCheck.IsGrounded && player.PlayerValues.Horiz != 0)
            {
                stateMachine.ChangeState(typeof(PlayerRunState));
            }
            else if (player.WallCheck.AtWall && player.PlayerValues.Horiz != 0)
            {
                stateMachine.ChangeState(typeof(PlayerWallSlideState));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift) && player.DashCount == 0)
            {
                stateMachine.ChangeState(typeof(PlayerDashState));
            }
            else if (player.JumpBufferTimer >= 0 && player.CoyoteTimer > 0 && !player.CameFromWall)
            {
                player.SetCoyoteTimerToZero();
                player.SetJumpBufferTimerToZero();
                stateMachine.ChangeState(typeof(PlayerJumpState));
            }
            else if(player.JumpBufferTimer >= 0 && player.CoyoteTimer > 0 && player.CameFromWall)
            {
                stateMachine.ChangeState(typeof(PlayerWallJumpState));
            }
        }

        public override void Exit()
        {
            base.Exit();
            player.PlayerAnim.SetFallingParam(false);
            player.CameFromWall = false;
        }
    }
}
