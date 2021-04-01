using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class GravityMultiplier
    {
        private readonly bool isPlayer;

        private readonly Rigidbody2D rb;

        private readonly EntityPush push;

        private const float LOW_JUMP_MULTIPLIER = 2f;
        private const float FALL_MULTIPLIER = 3f;

        public GravityMultiplier(Rigidbody2D _rb, bool _isPlayer, EntityPush _push)
        {
            rb = _rb;
            isPlayer = _isPlayer;
            push = _push;
        }

        public void MultiplyGravity()
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (FALL_MULTIPLIER - 1) * Time.deltaTime;
            }
            if (isPlayer)
            {
                if (rb.velocity.y > 0 && !Input.GetButton("Jump") || push.IsPushing)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (LOW_JUMP_MULTIPLIER - 1) * Time.deltaTime;
                }
            }
        }
    }
}
