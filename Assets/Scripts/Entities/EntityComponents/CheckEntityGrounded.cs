using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class CheckEntityGrounded
    {
        private readonly Rigidbody2D rb;
        private readonly LayerMask floor;
        private readonly BoxCollider2D boxCollider;
        private readonly float distance;

        public bool IsGrounded { get; set; }

        public Action OnGrounded { get; set; }
        public Action WhenNotGrounded { get; set; }

        public CheckEntityGrounded(Rigidbody2D rb, LayerMask floor, BoxCollider2D entityBoxCollider, float _distance)
        {
            this.rb = rb;
            this.floor = floor;
            boxCollider = entityBoxCollider;
            distance = _distance;
        }

        public void GroundCheck()
        {
            if (rb.velocity.y <= 0)
            {
                RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, distance, floor);

                if (hit)
                {
                    IsGrounded =  true;
                    OnGrounded?.Invoke();
                }
                else
                {
                    WhenNotGrounded?.Invoke();
                    IsGrounded = false;
                }
            }
        }

        
    }

    public interface IGrounded
    {
        void OnGrounded();
        void WhenNotGrounded();
    }
}
    
