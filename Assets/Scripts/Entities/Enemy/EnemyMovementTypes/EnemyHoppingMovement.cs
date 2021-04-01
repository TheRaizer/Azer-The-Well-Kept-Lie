using Azer.EntityComponents;
using Azer.UtilityComponents;
using UnityEngine;

namespace Azer.GeneralEnemy
{
    public class EnemyHoppingMovement
    {
        private float jumpForceX;

        #region Dependencies

        private readonly Rigidbody2D rb;
        private readonly IJump jump;
        private readonly CalculateDirectionToEntity dirToPlayer;
        private readonly EntityChangeMovementDirection changeMovement;
        private readonly CheckEntityGrounded groundCheck;
        private readonly FlipSprite flipSprite;
        private readonly Transform playerTransform;
        private readonly Transform enemyTransform;

        #endregion


        public EnemyHoppingMovement(Rigidbody2D _rb, IJump _jump, CheckEntityGrounded _groundCheck, FlipSprite _flipSprite, Transform _playerTransform, Transform _transform)
        {
            rb = _rb;
            jump = _jump;
            groundCheck = _groundCheck;
            flipSprite = _flipSprite;
            playerTransform = _playerTransform;
            enemyTransform = _transform;

            dirToPlayer = new CalculateDirectionToEntity();
            changeMovement = new EntityChangeMovementDirection();
        }

        public void SetJumpForceX(float _jumpForceX)
        {
            jumpForceX = _jumpForceX;
        }

        public void JumpRoam()
        {
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                jump.Jump();

                if (groundCheck.IsGrounded)
                    groundCheck.IsGrounded = false;
            }
        }

        public void JumpAggro()
        {
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                jumpForceX = changeMovement.ChangeSpeedToDirection(dirToPlayer.DirToEntity(enemyTransform, playerTransform)) * Mathf.Abs(jumpForceX);

                flipSprite.FlipTransformScale(jumpForceX);

                jump.JumpForceX = jumpForceX;

                jump.Jump();

                if (groundCheck.IsGrounded)
                    groundCheck.IsGrounded = false;
            }
        }

        public void ChangeJumpDirection()
        {
            jump.JumpForceX *= -1;
            flipSprite.FlipTransformScale(jump.JumpForceX);
        }
    }
}
