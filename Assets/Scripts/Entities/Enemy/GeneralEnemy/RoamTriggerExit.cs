using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.GeneralEnemy
{
    public class RoamTriggerExit : MonoBehaviour
    {
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GetComponentInParent<IAggroRange>().OnExit();
            }
        }
    }
}
