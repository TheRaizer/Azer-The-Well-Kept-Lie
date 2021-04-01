using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.Player;
using Azer.UtilityComponents;

namespace Azer.EntityComponents
{
    public class PsychokinesisControllableObject : MonoBehaviour, IPsychoKinesisControllable
    {
        private MoveableObjectVelocityToPosition moveThroughVelocity;
        private Rigidbody2D rb;
        private GravityMultiplier gravity;
        private TeleportToPositionOnCollisionWithBoundary teleportBackToStarting;
        private MoveableObjectDissolve dissolve;


        [SerializeField] private Camera _camera;
        [SerializeField] private float pushSpeed = 1f;

        [field: SerializeField] public float ReductionAmt { get; private set; }
        [field: SerializeField] public bool Follow { get; private set; } = false;


        private float gravityScale = 2.2f;

        void Awake()
        {
            _camera = _camera != null ? _camera : Camera.main;

            dissolve = GetComponent<MoveableObjectDissolve>();
            moveThroughVelocity = GetComponent<MoveableObjectVelocityToPosition>();
            rb = GetComponent<Rigidbody2D>();

            gravity = new GravityMultiplier(rb, false, null);
            teleportBackToStarting = GetComponentInChildren<TeleportToPositionOnCollisionWithBoundary>();

            gravityScale = rb.gravityScale;
        }

        private void Update()
        {
            CheckForMovement();

            if (teleportBackToStarting.CanTeleport && !dissolve.IsDissolving)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0.1f;
                StopFollowing();
                dissolve.StartDissolving();
            }
            if(dissolve.HasDissolved && !dissolve.IsResolving)
            {
                teleportBackToStarting.Teleport();
                dissolve.StartResolving();
            }
            if(dissolve.HasResolved)
            {
                dissolve.HasResolved = false;
                rb.gravityScale = gravityScale;
            }
        }

        private void FixedUpdate()
        {
            if(dissolve.HasDissolved)
                gravity.MultiplyGravity();
        }

        private void MoveToMouse()
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            moveThroughVelocity.MoveToPosition(mousePos);
        }

        public void PushThroughKinesis()
        {
            Follow = false;
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (mousePos - (Vector2) transform.position).normalized;

            rb.AddForce(new Vector2(dir.x, dir.y) * pushSpeed, ForceMode2D.Impulse);
        }

        public void CheckForMovement()
        {
            if (Follow)
            {
                MoveToMouse();
            }
        }

        public void StopFollowing()
        {
            Follow = false;
        }

        public void StartFollowing()
        {
            Follow = true;
        }
    }

    public interface IPsychoKinesisControllable
    {
        void StartFollowing();
        void StopFollowing();

        void PushThroughKinesis();
        
        bool Follow { get; }
        float ReductionAmt { get; }
        
    }
}
