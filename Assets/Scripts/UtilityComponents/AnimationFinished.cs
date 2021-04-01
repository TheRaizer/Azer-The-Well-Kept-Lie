using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class AnimationFinished : MonoBehaviour
    {
        private Animator anim;

        void Awake()
        {
            anim = GetComponent<Animator>(); 
        }

        void Update()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}