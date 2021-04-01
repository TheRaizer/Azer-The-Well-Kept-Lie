using System;
using UnityEngine;

namespace Azer.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void SetSpeedParam(int i)
        {
            anim.SetInteger("Speed", i);
        }
        public void SetSwingCountParam(int i)
        {
            if (i > 3 || i < 0)
            {
                Debug.Log(i);
                throw new ArgumentOutOfRangeException();
            }
            anim.SetInteger("SwingCount", i);
        }
        public void SetWallSlidingParam(bool b)
        {
            anim.SetBool("WallSliding", b);
        }
        public void SetIsGroundedParam(bool b)
        {
            anim.SetBool("IsGrounded", b);
        }
        public void SetFallingParam(bool b)
        {
            anim.SetBool("Falling", b);
        }
        public void SetJumpParam(bool b)
        {
            anim.SetBool("Jump", b);
        }
        public void SetDashParam(bool b)
        {
            anim.SetBool("Dash", b);
        }
        public void SetKnockbackParam(bool b)
        {
            anim.SetBool("KnockBack", b);
        }
    }
}
