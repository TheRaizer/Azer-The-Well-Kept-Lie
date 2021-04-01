using Azer.GeneralEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPauseLogic
{
    private readonly float pauseMin = 0;
    private readonly float pauseMax = 0;

    public bool Pausing { get; private set; }

    private float pauseTimer = 0f;

    public EnemyPauseLogic(float _pauseMin, float _pauseMax)
    {
        pauseMin = _pauseMin;
        pauseMax = _pauseMax;
    }

    public void StartTimer(float givenPauseTime)
    {
        if (!Pausing)
        {
            Pausing = true;
            float pauseTime = givenPauseTime == 0 ? Random.Range(pauseMin, pauseMax) : givenPauseTime;

            pauseTimer = pauseTime;
        }
    }

    public void PauseTimer()
    {
        if (Pausing) 
        { 
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                Pausing = false;
            }
        }
    }

    public void UnPause()
    {
        Pausing = false;
    }
}
