using System.Collections;
using UnityEngine;
using Azer.EntityComponents;
using Azer.States;

namespace Azer.GeneralEnemy
{
    public class EnemyKnockBack : MonoBehaviour, IKnockBack, IKeepEnumeratorToStop
    {
        private Rigidbody2D rb;
        public EntityPush Knock { get; private set; }
        private IChangeToKnockState knockBackState;

        public IEnumerator PushCoroutine { get; private set; }

        [SerializeField]
        public float knockBackXMultiplier,
                     knockBackYMultiplier;

        private void Awake()
        {
            knockBackState = GetComponent<IChangeToKnockState>();
            rb = GetComponent<Rigidbody2D>();

            Knock = new EntityPush(rb);
        }

        public void GroundKnock(Transform tr, float knockBackX, float knockBackY, float knockTime)
        {
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                Vector2 dir = transform.position - tr.position;
                int i = dir.x > 0 ? 1 : -1;
                rb.velocity = Vector2.zero;

                PushCoroutine = Knock.PushBothAxis(knockTime, i, knockBackX * knockBackXMultiplier, knockBackY * knockBackYMultiplier);
                knockBackState.ChangeToKnockBackState();
            }
        }
    }
}
