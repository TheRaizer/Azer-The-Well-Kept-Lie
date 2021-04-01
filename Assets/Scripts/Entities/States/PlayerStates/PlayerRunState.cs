using Azer.EntityComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.Player;

namespace Azer.States
{
    public class PlayerRunState : PlayerMoveableState
    {
        private readonly PlayerController player;
        private readonly Rigidbody2D rb;

        public PlayerRunState(StateMachine _stateMachine, PlayerController _player, Rigidbody2D _rb) : base (_stateMachine, _player, _rb)
        {
            player = _player;
            rb = _rb;
        }
        public override void Enter()
        {
            base.Enter();

            player.PlayerAnim.SetSpeedParam(1);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (player.PlayerValues.Horiz == 0)
            {
                stateMachine.ChangeState(typeof(PlayerIdleState));
            }
            else if (player.JumpBufferTimer >= 0 && player.CoyoteTimer > 0)
            {
                player.SetCoyoteTimerToZero();
                player.SetJumpBufferTimerToZero();
                stateMachine.ChangeState(typeof(PlayerJumpState));
            }
            else if (Input.GetMouseButtonDown(0))
            {
                stateMachine.ChangeState(typeof(PlayerSwingState));
            }
            else if (rb.velocity.y < 0 && !player.GroundCheck.IsGrounded)
            {
                stateMachine.ChangeState(typeof(PlayerFallingState));
            }
        }
    }
}
