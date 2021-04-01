using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;
using Azer.States;

namespace Azer.Player
{
    public class PlayerKnockBackLogic : MonoBehaviour, IKnockBack, IKeepEnumeratorToStop
    {
        [field: SerializeField] public bool CanKnockBack { get; set; } = true;

        [SerializeField] private float invicibilityTime = 0;

        private float spriteBlinkingTimer = 0.0f;
        private readonly float spriteBlinkingMiniDuration = 0.15f;
        private float spriteBlinkingTotalTimer = 0.0f;
        private float spriteBlinkingTotalDuration = 0f;
        private bool startBlinking = false;


        private PlayerHealthManager health;
        public EntityPush Knock { get; private set; }
        private Rigidbody2D rb;
        private SpriteRenderer sprite;
        private IChangeToKnockState knockBackState;

        private WaitForSeconds invincibilityYield;

        public IEnumerator PushCoroutine { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            invincibilityYield = new WaitForSeconds(invicibilityTime);
            spriteBlinkingTotalDuration = invicibilityTime;

            sprite = GetComponent<SpriteRenderer>();
            knockBackState = GetComponent<IChangeToKnockState>();
            rb = GetComponent<Rigidbody2D>();

            Knock = new EntityPush(rb);

            health = GetComponent<PlayerHealthManager>();
        }

        private void Update()
        {
            if (startBlinking)
            {
                SpriteBlinkingEffect();
            }
        }

        private void SpriteBlinkingEffect()
        {
            spriteBlinkingTotalTimer += Time.deltaTime;
            if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
            {
                startBlinking = false;
                spriteBlinkingTotalTimer = 0.0f;
                sprite.enabled = true;

                return;
            }

            spriteBlinkingTimer += Time.deltaTime;
            if (spriteBlinkingTimer < spriteBlinkingMiniDuration) return;

            spriteBlinkingTimer = 0.0f;
            if (sprite.enabled == true)
            {
                sprite.enabled = false;
            }
            else
            {
                sprite.enabled = true;
            }
        }

        public void GroundKnock(Transform tr, float knockBackX, float knockBackY, float knockTime)
        {
            if (CanKnockBack)
            {
                CanKnockBack = false;

                Vector2 dir = transform.position - tr.position;
                int i = dir.x > 0 ? 1 : -1;

                PushCoroutine = Knock.PushBothAxis(knockTime, i, knockBackX, knockBackY);

                knockBackState.ChangeToKnockBackState();

                StartCoroutine("InvicibilityTimeCo");
            }
        }

        private IEnumerator InvicibilityTimeCo()
        {
            startBlinking = true;
            yield return invincibilityYield;

            CanKnockBack = true;
            health.CanHit = true;
        }

        private void TurnOffKnockBack()//animator events
        {
            CanKnockBack = false;
        }

        private void TurnOnKnockBack()
        {
            CanKnockBack = true;
        }
    }
}
