using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class SetParticleSystemOff : MonoBehaviour
    {
        private ParticleSystem particle;

        // Start is called before the first frame update
        void Start() => particle = GetComponent<ParticleSystem>();


        // Update is called once per frame
        void Update()
        {
            if (particle.isStopped)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
