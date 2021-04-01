using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.GeneralEnemy
{
    public class SlimeAnimator : MonoBehaviour
    {
        private Animator anim;
        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        public void SetSlimeState(int i)
        {
            if(i < 0 || i > 2)
            {
                Debug.Log(i);
                throw new ArgumentOutOfRangeException();
            }

            anim.SetInteger("SlimeState", i);
        }
    }
}
