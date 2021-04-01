using Azer.EntityComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.Mechanisms
{
    public class InstaKillOnRaycast : MonoBehaviour
    {
        [SerializeField] private float distance = 0f;
        [SerializeField] private Transform raycastLaunchPoint = null;
        [SerializeField] private LayerMask layer = 1 << 1;
        [SerializeField] private bool shootHoriz = true;

        private RaycastHit2D hit;
        public bool ShootRay { get; set; }

        private void InstaKillRayCast()
        {
            if (shootHoriz)
            {
                hit = Physics2D.Raycast(raycastLaunchPoint.position, Vector2.right, distance, layer);
                Debug.DrawRay(raycastLaunchPoint.position, Vector2.right * distance, Color.red);
            }
            else
            {
                hit = Physics2D.Raycast(raycastLaunchPoint.position, Vector2.up, distance, layer);
                Debug.DrawRay(raycastLaunchPoint.position, Vector2.up * distance, Color.red);
            }


            if (hit)
            {
                if (hit.collider.GetComponentInParent<HealthManagerTemplate>() != null)
                {
                    hit.collider.GetComponentInParent<HealthManagerTemplate>().InstaKill();
                }
            }
        }

        private void Update()
        {
            InstaKillRayCast();
        }
    }
}
