using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaizeLeaperAnimator : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(int i)
    {
        anim.SetInteger("Speed", i);
    }

    public void SetIdleTick(int i)
    {
        if(i > 2 || i < 0)
        {
            Debug.Log(i);
            throw new ArgumentOutOfRangeException();
        }
        anim.SetInteger("IdleTick", i);
    }

    public void SetLeapStage(int i)
    {
        if (i > 2 || i < 0)
        {
            Debug.Log(i);
            throw new ArgumentOutOfRangeException();
        }
        anim.SetInteger("LeapStage", i);
    }
}
