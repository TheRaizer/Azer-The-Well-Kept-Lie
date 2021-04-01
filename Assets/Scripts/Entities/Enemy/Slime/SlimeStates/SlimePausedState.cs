using Azer.GeneralEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.States
{
    public class SlimePausedState : State
    {
        private readonly Rigidbody2D rb;
        private readonly SlimeController slimeController;

        public SlimePausedState(StateMachine _stateMachine, Rigidbody2D _rb, SlimeController _controller) : base(_stateMachine)
        {
            rb = _rb;
            slimeController = _controller;
        }

        public override void Enter()
        {
            base.Enter();

            rb.velocity = Vector2.zero;
            slimeController.SlimeAnim.SetSlimeState(2);

            if (slimeController.ParentEnemy.InAggro)
            {
                slimeController.PauseLogic.StartTimer(slimeController.AggroPause);
            }
            else
            {
                slimeController.PauseLogic.StartTimer(0);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            slimeController.PauseLogic.PauseTimer();

            if (slimeController.WallCheck.AtWall || slimeController.EdgeCheck.AtEdge)
            {
                slimeController.JumpLogic.ChangeJumpDirection();
            }
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(!slimeController.PauseLogic.Pausing)
            {
                if (slimeController.ParentEnemy.InAggro)
                {
                    stateMachine.ChangeState(typeof(SlimeJumpAggroState));
                }
                else
                    stateMachine.ChangeState(typeof(SlimeJumpRoamState));
            }
        }

        public override void Exit()
        {
            base.Exit();

            slimeController.PauseLogic.UnPause();
        }
    }
}
