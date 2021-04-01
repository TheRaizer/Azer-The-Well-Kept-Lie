using System;
using System.Collections;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class EntityPush
    {
        public bool IsPushing { get; set; }

        public Action OnPushStart { get; set; }
        public Action OnPushEnd { get; set; }

        private readonly Rigidbody2D rb;

        public EntityPush(Rigidbody2D _rb)
        {
            rb = _rb;
        }

        public IEnumerator PushVert(float pushTime, float vertPushSpeed)
        {
            if (!IsPushing)
            {
                StartPush();

                rb.velocity = new Vector2(rb.velocity.x, vertPushSpeed);

                yield return new WaitForSeconds(pushTime);

                EndPush();
            }
        }

        public IEnumerator PushBothAxis(float pushTime, int pushDirection, float diagonalPushSpeedX, float diagonalPushSpeedY)
        {
            if (!IsPushing)
            {
                StartPush();

                rb.velocity = new Vector2(diagonalPushSpeedX * pushDirection, diagonalPushSpeedY);

                yield return new WaitForSeconds(pushTime);

                EndPush();
            }
        }

        public void StartPush()
        {
            //make sure to enter a restriction state for the entity
            IsPushing = true;
            OnPushStart?.Invoke();
        }
        public void EndPush()
        {
            //make sure to exit a restriction state for the entity
            IsPushing = false;
            OnPushEnd?.Invoke();
        }
    }

    public interface IKeepEnumeratorToStop
    {
        IEnumerator PushCoroutine { get; }
    }

    public interface IKnockBack
    {
        void GroundKnock(Transform tr, float knockBackX, float knockBackY, float knockTime);
    }
}
