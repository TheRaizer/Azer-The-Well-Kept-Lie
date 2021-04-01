using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Azer.UtilityComponents
{
    public class ChangeCameraOptions : MonoBehaviour 
    {
        [SerializeField]
        private CameraController cameraController = null;

        private Vector2 enterDir,
                        exitDir;

        [field: SerializeField] public Transform NextAnchor { get; private set; }
        [field: SerializeField] public Transform PrevAnchor { get; private set; }

        [field: SerializeField] public float NextMinX { get; private set; }
        [field: SerializeField] public float NextMinY { get; private set; }
        [field: SerializeField] public float NextMaxX { get; private set; }
        [field: SerializeField] public float NextMaxY { get; private set; }

        [SerializeField]
        private float prevMinX = 0,
                      prevMinY = 0,
                      prevMaxX = 0,
                      prevMaxY = 0;

        [field: SerializeField] public float NextXOffset { get; private set; }
        [field: SerializeField] public float NextYOffset { get; private set; }

        [SerializeField]
        private float prevXOffset = 0,
                      prevYOffset = 0;

        [field: SerializeField] public float nextSmoothedSpeed = 4;

        [SerializeField] private float prevSmoothedSpeed = 4;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                enterDir = collision.transform.position - transform.position;

                if (enterDir.x < 0)
                {
                    cameraController.SetOffsets(NextXOffset, NextYOffset);
                    cameraController.SetBounds(NextMinX, NextMinY, NextMaxX, NextMaxY);
                    cameraController.SetLerpSpeed(nextSmoothedSpeed);

                    if (NextAnchor != null)
                    {
                        cameraController.SetAnchor(NextAnchor);
                    }
                }
                else if (enterDir.x > 0)
                {
                    cameraController.SetAnchor(PrevAnchor);
                    cameraController.SetOffsets(prevXOffset, prevYOffset);
                    cameraController.SetBounds(prevMinX, prevMinY, prevMaxX, prevMaxY);
                    cameraController.SetLerpSpeed(prevSmoothedSpeed);

                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                exitDir = collision.transform.position - transform.position;

                if (enterDir.x < 0 && exitDir.x < 0)
                {
                    cameraController.SetAnchor(PrevAnchor);
                    cameraController.SetOffsets(prevXOffset, prevYOffset);
                    cameraController.SetBounds(prevMinX, prevMinY, prevMaxX, prevMaxY);
                    cameraController.SetLerpSpeed(prevSmoothedSpeed);
                }
                else if (enterDir.x > 0 && exitDir.x > 0)
                {
                    cameraController.SetAnchor(NextAnchor);
                    cameraController.SetOffsets(NextXOffset, NextYOffset);
                    cameraController.SetBounds(NextMinX, NextMinY, NextMaxX, NextMaxY);
                    cameraController.SetLerpSpeed(nextSmoothedSpeed);
                }
            }
        }
    }
}
