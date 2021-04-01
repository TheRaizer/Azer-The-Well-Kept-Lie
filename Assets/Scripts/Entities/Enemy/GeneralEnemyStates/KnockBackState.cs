using Azer.EntityComponents;
using Azer.GeneralEnemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.States
{
    public class KnockBackState : State
    {
        private readonly EnemyKnockBack enemyKnockBack;
        private readonly CheckEntityGrounded groundCheck;
        private readonly Type afterKnockState;

        public KnockBackState(StateMachine _stateMachine, EnemyKnockBack _enemyKnockBack, CheckEntityGrounded _groundCheck, Type _afterKnockState) : base (_stateMachine)
        {
            enemyKnockBack = _enemyKnockBack;
            groundCheck = _groundCheck;
            afterKnockState = _afterKnockState;
        }

        public override void Enter()
        {
            base.Enter();
            
            enemyKnockBack.StartCoroutine(enemyKnockBack.PushCoroutine);

            if(groundCheck.IsGrounded)
                groundCheck.IsGrounded = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(groundCheck.IsGrounded)
            {
                stateMachine.ChangeState(afterKnockState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            enemyKnockBack.Knock.EndPush();
            enemyKnockBack.StopCoroutine(enemyKnockBack.PushCoroutine);
        }
    }
}
