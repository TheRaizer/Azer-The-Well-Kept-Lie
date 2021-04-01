using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.Mechanisms
{
    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] pointsToMoveThrough = null;

        [SerializeField] private float speed = 0;
        private int currentIndex = 0;


        void Update()
        {
            MoveThroughPoints();
        }

        private void MoveThroughPoints()
        {
            transform.position = Vector3.MoveTowards(transform.position, pointsToMoveThrough[currentIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pointsToMoveThrough[currentIndex].position) < 0.2f)
            {
                ChangePointToMoveTo();
            }
        }

        private void ChangePointToMoveTo()
        {
            if (currentIndex < pointsToMoveThrough.Length - 1)
            {
                currentIndex++;
            }
            else
                currentIndex = 0;
        }
    }
}
