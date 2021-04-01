using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class CameraShake : MonoBehaviour
    {
        private float shakeAmount, shakeTimeRemaining, shakeFadeTime, shakeRotation;

        [SerializeField] private float rotationMultiplier = 0f;

        private void LateUpdate()
        {
            Shake();
        }

        public void BeginShake(float amt, float length)
        {
            shakeAmount = amt;

            shakeTimeRemaining = length;

            shakeFadeTime = amt / length;

            shakeRotation = amt * rotationMultiplier;
        }

        private void Shake()
        {
            if (shakeTimeRemaining > 0)
            {
                shakeTimeRemaining -= Time.deltaTime;
                float xAmt = Random.Range(-1f, 1f) * shakeAmount;
                float yAmt = Random.Range(-1f, 1f) * shakeAmount;

                transform.position += new Vector3(xAmt, yAmt, 0f);

                shakeAmount = Mathf.MoveTowards(shakeAmount, 0f, shakeFadeTime * Time.deltaTime);

                shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
            }

            transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
        }
    }
}
