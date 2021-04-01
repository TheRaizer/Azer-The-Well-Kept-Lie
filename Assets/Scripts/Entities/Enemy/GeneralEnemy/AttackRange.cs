using Azer.EntityComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Transform attackRaycastPoint = null;
    [SerializeField] private float checkDistance = 0.5f;
    [SerializeField] private LayerMask playerLayer = 1 << 1;

    public bool InAttackRange { get; private set; }

    public FlipSprite flipSprite;

    private void Awake()
    {
        flipSprite = GetComponent<FlipSprite>();
    }

    public void CheckIfInRange()
    {
        RaycastHit2D hit;

        if (flipSprite.FacingRight)
        {
            hit = Physics2D.Raycast(attackRaycastPoint.position, Vector2.right, checkDistance, playerLayer);

            Debug.DrawRay(attackRaycastPoint.position, Vector2.right * checkDistance, Color.red);
        }
        else
        {
            hit = Physics2D.Raycast(attackRaycastPoint.position, Vector2.left, checkDistance, playerLayer);
            Debug.DrawRay(attackRaycastPoint.position, Vector2.left * checkDistance, Color.red);
        }


        if (hit.collider != null)
        {
            InAttackRange = true;
            return;
        }

        InAttackRange = false;
    }
}
