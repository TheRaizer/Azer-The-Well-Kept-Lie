using Azer.EntityComponents;
using Azer.GeneralEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.States
{
    public class RaizeLeaperLeapState : State, IKeepEnumeratorToStop
    {
        private readonly RaizeLeaperController controller;
        private readonly EntityPush leap;
        private readonly FlipSprite flipSprite;

        public IEnumerator PushCoroutine { get; private set; }

        public RaizeLeaperLeapState(StateMachine _stateMachine, RaizeLeaperController _controller, EntityPush _leap, FlipSprite _flipSprite) : base(_stateMachine)
        {
            controller = _controller;
            leap = _leap;
            flipSprite = _flipSprite;
        }

        public override void Enter()
        {
            base.Enter();

            controller.RaizeAnim.SetLeapStage(2);

            if (flipSprite.FacingRight)
                PushCoroutine = leap.PushBothAxis(controller.LeapTime, 1, controller.LeapAmt, 0);
            else
                PushCoroutine = leap.PushBothAxis(controller.LeapTime, -1, controller.LeapAmt, 0);

            controller.StartCoroutine(PushCoroutine);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(!leap.IsPushing)
            {
                if(controller.ParentEnemy.InAggro)
                {
                    stateMachine.ChangeState(typeof(RaizeLeaperAggro));
                }
                else
                    stateMachine.ChangeState(typeof(RaizeLeaperRoamState));
            }
        }

        public override void Exit()
        {
            base.Exit();

            controller.RaizeAnim.SetLeapStage(0);
            leap.EndPush();
            controller.StopCoroutine(PushCoroutine);
        }
    }
}
