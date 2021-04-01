
using System.Collections;
using System.Collections.Generic;
using Azer.Player;
using UnityEngine;

namespace Azer.States
{
    public class PlayerKnockBackState : State
    {
        private readonly PlayerKnockBackLogic knockBack;
        private readonly PlayerController player;
        private readonly Rigidbody2D rb;

        public PlayerKnockBackState(StateMachine _stateMachine, PlayerController _player, PlayerKnockBackLogic _knockBack, Rigidbody2D _rb) : base(_stateMachine)
        {
            player = _player;
            knockBack = _knockBack;
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();

            player.StartCoroutine(knockBack.PushCoroutine);

            if(player.GroundCheck.IsGrounded)
                player.GroundCheck.IsGrounded = false;

            player.PlayerAnim.SetKnockbackParam(true);
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
            else if (rb.velocity.y < 0 && !knockBack.Knock.IsPushing && !player.GroundCheck.IsGrounded)
            {
                stateMachine.ChangeState(typeof(PlayerFallingState));
            }
        }

        public override void Exit()
        {
            base.Exit();

            knockBack.Knock.EndPush();
            player.StopCoroutine(knockBack.PushCoroutine);
            player.PlayerAnim.SetKnockbackParam(false);
        }
    }
}
