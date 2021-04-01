using System.Collections;
using UnityEngine;

namespace Azer.Player
{
    public class PlayerSwing : MonoBehaviour
    {
        [field: SerializeField] public int SwingCount { get; private set; }

        private int animSwingCount = 0;

        private bool registerNext = true;
        private bool registered = false;
        private bool pushing = false;
        public bool Swinging { get; private set; }

        private readonly int maxNumberOfSwings = 3;

        [SerializeField]
        private float secondSwingPush = 0f,
                      secondPushLength = 0f,
                      thirdSwingPush = 0f,
                      thirdPushLength = 0f;


        private PlayerAnimator playerAnim;
        private PlayerController player;
        private Rigidbody2D rb;
        private PlayerKnockBackLogic knockBack;

        void Awake()
        {
            knockBack = GetComponent<PlayerKnockBackLogic>();
            player = GetComponent<PlayerController>();
            rb = GetComponent<Rigidbody2D>();
            playerAnim = GetComponent<PlayerAnimator>();
        }

        private void Attack()
        {
            playerAnim.SetSwingCountParam(animSwingCount);
        }

        public void ResetSwingCount()
        {
            SwingCount = 0;
            animSwingCount = 0;
            playerAnim.SetSwingCountParam(animSwingCount);
        }

        private IEnumerator MovePlayerOnSwingCo(float pushAmt, float pushLength)
        {
            if (pushing) yield break;

            if (player.GroundCheck.IsGrounded)
            {
                pushing = true;
                rb.velocity = new Vector2(pushAmt, rb.velocity.y);

                yield return new WaitForSeconds(pushLength);

                if (!knockBack.Knock.IsPushing)
                {
                    rb.velocity = Vector2.zero;
                }
                pushing = false;
            }
        }

        public void OnInput()
        {
            if (registerNext && !registered)
            {
                Swinging = true;
                if (animSwingCount > maxNumberOfSwings)
                {
                    ResetSwingCount();
                }

                animSwingCount++;
                registered = true;
                Attack();
            }
        }
        public void CanRegisterNextSwing()
        {
            registerNext = true;
            registered = false;
        }
        public void NotSwinging() => Swinging = false;

        //ANIMATOR FUNCTIONS

        private void WontRegisterNextSwing() => registerNext = false;
        private void StartOfSwing() => SwingCount = animSwingCount;


        private void PushPlayerOnSecondSwing()
        {
            int dir = player.FlipSprite.FacingRight ? 1 : -1;

            StartCoroutine(MovePlayerOnSwingCo(secondSwingPush * dir, secondPushLength));
        }

        private void PushPlayerOnThirdSwing()
        {
            int dir = player.FlipSprite.FacingRight ? 1 : -1;

            StartCoroutine(MovePlayerOnSwingCo(thirdSwingPush * dir, thirdPushLength));
        }

    }
}
