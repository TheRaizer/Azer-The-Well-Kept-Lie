using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;
using Azer.Player;

namespace Azer.States
{
    public class PlayerJumpState : PlayerMoveableState
    {
        private readonly PlayerController player;
        private readonly Rigidbody2D rb;
        private readonly IJump playerJump;


        public PlayerJumpState(StateMachine _stateMachine, PlayerController _player, IJump _jump, Rigidbody2D _rb) : base(_stateMachine, _player, _rb)
        {
            player = _player;
            playerJump = _jump;
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();


            player.PlayerAnim.SetJumpParam(true);

            playerJump.JumpForceX = player.PlayerValues.JumpForceX;
            playerJump.JumpForceY = player.PlayerValues.JumpForceY;
            playerJump.Jump();

            if(player.GroundCheck.IsGrounded)
                player.GroundCheck.IsGrounded = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if (Input.GetKeyDown(KeyCode.LeftShift) && player.DashCount == 0)
            {
                stateMachine.ChangeState(typeof(PlayerDashState));
            }
            else if (rb.velocity.y < 0 && !player.GroundCheck.IsGrounded)
            {
                stateMachine.ChangeState(typeof(PlayerFallingState));
            }
        }

        public override void Exit()
        {
            base.Exit();

            player.PlayerAnim.SetJumpParam(false);
        }
    }
}

