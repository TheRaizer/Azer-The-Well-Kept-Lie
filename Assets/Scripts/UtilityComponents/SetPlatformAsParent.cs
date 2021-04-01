using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class SetPlatformAsParent : MonoBehaviour//place script on trigger that will only collide with the moving platform layer
    {
        [SerializeField] Transform TopMostParentTransform = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TopMostParentTransform.SetParent(collision.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            TopMostParentTransform.transform.SetParent(null);
        }
    }
}
