using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform positionToLerp = null;
        [SerializeField] private float lerpSpeed = 0;


        [SerializeField]
        private float minX = 0,
                      minY = 0,
                      maxX = 0,
                      maxY = 0;
        [SerializeField]
        private float xOffset = 0,
                      yOffset = 0;

        [SerializeField] private bool hasBoundsOffsets = false;

        [field: SerializeField]
        public bool IsFollowing { get; set; } = true;


        private void FixedUpdate()
        {
            LerpPosition();
        }

        public void LerpPosition()
        {
            Vector3 smoothedPosition;

            if (IsFollowing)
            {
                if (hasBoundsOffsets)
                {
                    Vector3 desiredPosition = new Vector3(Mathf.Clamp(positionToLerp.position.x + xOffset, minX, maxX),
                                                          Mathf.Clamp(positionToLerp.position.y + yOffset, minY, maxY),
                                                          transform.position.z);
                    smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 desiredPosition = new Vector3(positionToLerp.position.x, positionToLerp.position.y, transform.position.z);

                    smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, lerpSpeed * Time.deltaTime);
                }

                transform.position = smoothedPosition;
            }
        }

        public void SetBounds(float minX, float minY, float maxX, float maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }
        public void SetOffsets(float xOffset, float yOffset)
        {
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public void FlipXOffset(int dir) => xOffset *= dir;

        public void SetAnchor(Transform anchor) => positionToLerp = anchor;
        public void SetLerpSpeed(float _lerpSpeed) => lerpSpeed = _lerpSpeed;
        public Transform GetAnchor() => positionToLerp;

    }
}
