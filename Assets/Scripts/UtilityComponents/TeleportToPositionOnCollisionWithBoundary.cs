using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class TeleportToPositionOnCollisionWithBoundary : MonoBehaviour
    {
        [SerializeField] private Transform positionToTeleportToo = null;
        [SerializeField] private Transform pos = null;
        [SerializeField] private Rigidbody2D rb = null;

        [field: SerializeField]
        public bool CanTeleport { get; set; }

        public void Teleport()
        {
            if (CanTeleport)
            {
                CanTeleport = false;
                rb.velocity = Vector2.zero;
                pos.position = (Vector2) positionToTeleportToo.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 23 && !CanTeleport)
            {
                CanTeleport = true;
            }
        }
    }
}
