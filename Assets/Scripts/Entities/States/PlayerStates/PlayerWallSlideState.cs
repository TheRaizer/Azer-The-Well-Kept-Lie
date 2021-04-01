using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.UtilityComponents;
using Azer.Player;

namespace Azer.States
{
    public class PlayerWallSlideState : State
    {
        private readonly PlayerController player;
        private readonly Rigidbody2D rb;

        public PlayerWallSlideState(StateMachine _stateMachine, PlayerController _player, Rigidbody2D _rb) : base(_stateMachine)
        {
            stateMachine = _stateMachine;
            player = _player;
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();
            rb.velocity = Vector2.zero;

            player.PlayerAnim.SetWallSlidingParam(true);
            player.CameFromWall = true;
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
            else if (rb.velocity.y < 0 && !player.GroundCheck.IsGrounded && 
                    (!player.WallCheck.AtWall || player.PlayerValues.Horiz == 0) 
                    || CheckSigns.SameSign(player.WallCheck.DirToWall.x, player.PlayerValues.Horiz))
            {
                stateMachine.ChangeState(typeof(PlayerFallingState));
            }
            else if (Input.GetButtonDown("Jump"))
            {
                stateMachine.ChangeState(typeof(PlayerWallJumpState));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            rb.velocity = new Vector2(rb.velocity.x, -player.PlayerValues.WallSlideSpeed);
        }

        public override void Exit()
        {
            base.Exit();

            player.PlayerAnim.SetWallSlidingParam(false);
            player.WallCheck.AtWall = false;
        }
    }
}
