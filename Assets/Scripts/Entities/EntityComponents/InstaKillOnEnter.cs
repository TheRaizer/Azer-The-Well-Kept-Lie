using System;
using UnityEngine;
using Azer.EntityComponents;

namespace Azer.EntityComponents
{
    public class InstaKillOnEnter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponentInParent<HealthManagerTemplate>() != null)
            {
                collision.GetComponentInParent<HealthManagerTemplate>().InstaKill();
            }
        }
    }
}
