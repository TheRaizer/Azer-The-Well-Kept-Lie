using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class EntityJump : IJump
    {
        public float JumpForceX { get; set; }
        public float JumpForceY { get; set; }

        private readonly Rigidbody2D rb;

        public Action OnJump { get; set; }

        public EntityJump(Rigidbody2D _rb)
        {
            rb = _rb;
        }

        public void Jump()
        {
            rb.velocity = new Vector2(JumpForceX, JumpForceY);
            OnJump?.Invoke();
        }
    }

    public interface IJump
    {
        float JumpForceX { get; set; }
        float JumpForceY { get; set; }
        void Jump();
        Action OnJump { get; set; }
    }
}