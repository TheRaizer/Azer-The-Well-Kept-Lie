using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{ 
    public class CrushEntity : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private float killVelocity = 0f;
        [SerializeField] private bool controllableByPlayer = true;
        [SerializeField] private bool onBothAxis = false;

        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Mathf.Abs(rb.velocity.y) >= killVelocity || Mathf.Abs(rb.velocity.x) >= killVelocity)
            {
                if (controllableByPlayer)
                {
                    if (collision.CompareTag("Enemy"))
                    {
                        collision.GetComponentInParent<HealthManagerTemplate>().InstaKill();
                    }
                }
            }
            else if(!onBothAxis)
            {
                if (controllableByPlayer && Mathf.Abs(rb.velocity.y) >= killVelocity)
                {
                    if (collision.CompareTag("Enemy"))
                    {
                        collision.GetComponentInParent<HealthManagerTemplate>().InstaKill();
                    }
                }
            }
        }
    }
}
