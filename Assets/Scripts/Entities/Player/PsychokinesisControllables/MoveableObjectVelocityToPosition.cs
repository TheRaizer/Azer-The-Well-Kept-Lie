using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class MoveableObjectVelocityToPosition : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private float slowAmount = 1f;

        void Awake() => rb = GetComponent<Rigidbody2D>();

        public void MoveToPosition(Vector2 positionToMoveTo)
        {
            Vector2 dir = (positionToMoveTo - (Vector2)transform.position) / slowAmount;
            rb.velocity = new Vector2(dir.x, dir.y);
        }
    }
}
