using Azer.UtilityComponents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class MoveToEntityVelocity
    {
        private readonly CalculateDirectionToEntity calcDirToEntity;
        private readonly EntityChangeMovementDirection changeMovementDir;
        private readonly FlipSprite flipSprite;
        private readonly Rigidbody2D rb;
        private readonly Transform entityToMoveToo;

        private readonly bool moveOnBothAxis;

        public MoveToEntityVelocity(Rigidbody2D _rb, Transform _entityToMoveToo, bool _moveOnBothAxis, FlipSprite _flipSprite)
        {
            flipSprite = _flipSprite;
            rb = _rb;
            moveOnBothAxis = _moveOnBothAxis;
            entityToMoveToo = _entityToMoveToo;

            calcDirToEntity = new CalculateDirectionToEntity();
            changeMovementDir = new EntityChangeMovementDirection();
        }

        public void MoveToEntity(float moveSpeed)
        {
            moveSpeed = changeMovementDir.ChangeSpeedToDirection(calcDirToEntity.DirToEntity(rb.transform, entityToMoveToo)) * Mathf.Abs(moveSpeed);

            flipSprite.FlipTransformScale(moveSpeed);

            if (moveOnBothAxis)
            {
                Vector2 dir = calcDirToEntity.DirToEntity(rb.transform, entityToMoveToo).normalized;
                rb.velocity = dir * Mathf.Abs(moveSpeed);
            }
            else
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
    }
}
