using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.Mechanisms
{
    public class LaserOnOff : MonoBehaviour
    {
        private ParticleSystem laser;

        private ParticleSystem.EmissionModule emission;

        [SerializeField] private float emissionDecreaseRate = 0f,
                                       emissionIncreaseRate = 0f;

        [SerializeField] private float stayOnTime = 0f,
                                       stayOffTime = 0f,
                                       decreaseTime = 0f;

        private float decreaseTimer = 0f;
        private float onTimer = 0f;
        private float offTimer = 0f;

        private float startRate = 0f;
        private InstaKillOnRaycast raycast;

        [SerializeField] private bool on = true;

        void Awake()
        {
            decreaseTimer = decreaseTime;

            laser = GetComponentInChildren<ParticleSystem>();
            raycast = GetComponent<InstaKillOnRaycast>();

            emission = laser.emission;
            startRate = emission.rateOverTimeMultiplier;

            onTimer = stayOnTime;
            offTimer = stayOffTime;
        }

        void Update()
        {
            if(!on)
            {
                DecreaseEmissionRate();
                if(!laser.isPlaying)
                {
                    StayOffTimer();
                }
            }
            else
            {
                IncreaseEmissionRate();
                StayOnTimer();
            }
            
        }

        public void DecreaseEmissionRate()
        {
            decreaseTimer -= Time.deltaTime;

            if (decreaseTimer > 0)
            {
                emission.rateOverTimeMultiplier = Mathf.Clamp(emission.rateOverTimeMultiplier - emissionDecreaseRate * Time.deltaTime, 0, startRate);
            }
            else if (decreaseTimer <= 0)
            {
                raycast.enabled = false;
                decreaseTimer = decreaseTime;
                emission.rateOverTimeMultiplier = 0;

                laser.Stop();
            }
        }

        public void IncreaseEmissionRate()
        {
            if (emission.rateOverTimeMultiplier < startRate)
                emission.rateOverTimeMultiplier = Mathf.Clamp(emission.rateOverTimeMultiplier + emissionIncreaseRate * Time.deltaTime, 0, startRate);

            else if (emission.rateOverTimeMultiplier > startRate)
                emission.rateOverTimeMultiplier = startRate;
        }


        private void StayOnTimer()
        {
            if (!laser.isPlaying)
            {
                raycast.enabled = true;
                laser.Play();
            }

            onTimer -= Time.deltaTime;

            if (onTimer <= 0)
            {
                onTimer = stayOnTime;
                on = false;
            }
        }

        private void StayOffTimer()
        {
            offTimer -= Time.deltaTime;

            if (offTimer <= 0)
            {
                on = true;
                offTimer = stayOffTime;
            }
        }
    }
}
