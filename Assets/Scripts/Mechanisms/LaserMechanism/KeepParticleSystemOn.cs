using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.Mechanisms
{
    public class KeepParticleSystemOn : MonoBehaviour
    {
        private ParticleSystem particles;

        private void Awake()
        {
            particles = GetComponentInChildren<ParticleSystem>();
        }

        public void Update()
        {
            if (!particles.isPlaying)
            {
                particles.Play();
            }
        }
    }
}
