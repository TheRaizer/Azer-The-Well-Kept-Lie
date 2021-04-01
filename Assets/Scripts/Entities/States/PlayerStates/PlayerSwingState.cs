using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.Player;

namespace Azer.States 
{
    public class PlayerSwingState : State
    {
        private readonly PlayerController player;
        private readonly PlayerSwing swing;
        private readonly Rigidbody2D rb;

        public PlayerSwingState(StateMachine _stateMachine, PlayerController _player, PlayerSwing _swing, Rigidbody2D _rb) : base(_stateMachine)
        {
            stateMachine = _stateMachine;
            player = _player;
            swing = _swing;
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();
            rb.velocity = Vector2.zero;

            swing.OnInput();
        }   

        public override void HandleInput()
        {
            base.HandleInput();

            if(Input.GetMouseButtonDown(0))
            {
                swing.OnInput();
            }

            if (!swing.Swinging && player.PlayerValues.Horiz == 0)
            {
                stateMachine.ChangeState(typeof(PlayerIdleState));
            }
            else if (!swing.Swinging && player.PlayerValues.Horiz != 0)
            {
                stateMachine.ChangeState(typeof(PlayerRunState));
            }
            else if (rb.velocity.y < 0 && !player.GroundCheck.IsGrounded)
            {
                stateMachine.ChangeState(typeof(PlayerFallingState));
            }
        }

        public override void Exit()
        {
            base.Exit();

            swing.ResetSwingCount();
            swing.NotSwinging();
            swing.CanRegisterNextSwing();
        }
    }
}
