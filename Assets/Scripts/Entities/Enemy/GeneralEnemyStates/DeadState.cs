using Azer.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.States
{
    public class DeadState : State
    {
        private readonly Rigidbody2D rb;
        public DeadState(StateMachine _stateMachine, Rigidbody2D _rb) : base (_stateMachine)
        {
            rb = _rb;
        }

        public override void Enter()
        {
            base.Enter();

            rb.velocity = Vector2.zero;
        }
    }
}
