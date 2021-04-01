using Azer.GeneralEnemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuallyOverrideEnemyAggroRaycast : MonoBehaviour
{
    [SerializeField] private EnemyAggroRaycast aggroRaycast = null;

    //animation events
    private void OverrideControlAggroRaycast()
    {
        aggroRaycast.ManuallyOverride();
    }
    private void UndoOverrideControlAggroRaycast()
    {
        aggroRaycast.UndoOverride();
    }
}
