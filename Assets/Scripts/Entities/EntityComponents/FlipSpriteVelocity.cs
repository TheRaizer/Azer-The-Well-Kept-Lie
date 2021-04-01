using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class FlipSpriteVelocity : MonoBehaviour
    {
        private Rigidbody2D rb;

        private FlipSprite flipSprite;

        private void Awake()
        {
            flipSprite = GetComponent<FlipSprite>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() => FlipSprite();

        private void FlipSprite()
        {
            flipSprite.FlipTransformScale(rb.velocity.x);
        }
    }
}
