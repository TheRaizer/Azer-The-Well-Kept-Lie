using Azer.EntityComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.GeneralEnemy
{
    public class EnemyWalkBetweenTwoPoints
    {
        private readonly Transform enemyTransform;
        private readonly FlipSprite flipSprite;
        private readonly Rigidbody2D rb;
        private readonly EntityChangeMovementDirection changeDirection;

        private Vector2 leftPoint,
                        rightPoint;

        private float pointToMoveToo = 0f;

        private float moveSpeed;

        public bool AtPoint { get; set; }

        public EnemyWalkBetweenTwoPoints(Transform _enemyTransform, FlipSprite _flipSprite, Rigidbody2D _rb, float _moveSpeed)
        {
            enemyTransform = _enemyTransform;
            flipSprite = _flipSprite;
            rb = _rb;
            moveSpeed = _moveSpeed;

            changeDirection = new EntityChangeMovementDirection();
        }

        public void Movement()
        {
            if (Mathf.Abs(enemyTransform.position.x - pointToMoveToo) > 0.4f)
            {
                Vector2 dir = new Vector2(pointToMoveToo - enemyTransform.position.x, 0);
                moveSpeed = changeDirection.ChangeSpeedToDirection(dir) * Mathf.Abs(moveSpeed);
                
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                flipSprite.FlipTransformScale(rb.velocity.x);
            }
            else
            {
                AtPoint = true;
            }
        }

        public void GeneratePoint()//generate on pause start
        {
            pointToMoveToo = UnityEngine.Random.Range(leftPoint.x, rightPoint.x);

            if (Mathf.Abs(enemyTransform.position.x - pointToMoveToo) < 1)
            {
                pointToMoveToo = Mathf.Clamp(pointToMoveToo + 1.5f, leftPoint.x, rightPoint.x);
            }
        }

        public void SetLeftRightPoints(Vector2 left, Vector2 right)
        {
            leftPoint = left;
            rightPoint = right;
        }
    }
}
