using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class FlipSprite : MonoBehaviour
    {
        [field: SerializeField] public bool FacingRight { get; private set; } = true;

        public void FlipTransformScale(float speed)
        {
            if (FacingRight && speed < 0 || !FacingRight && speed > 0)
            {
                FacingRight = !FacingRight;
                Vector2 scale = transform.localScale;

                scale.x *= -1;

                transform.localScale = scale;
            }
        }
    }
}
