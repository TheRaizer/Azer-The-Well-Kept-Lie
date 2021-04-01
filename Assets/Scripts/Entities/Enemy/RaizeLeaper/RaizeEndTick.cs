using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaizeEndTick : MonoBehaviour
{
    public bool Tick { get; set; }

    public void EndTick()
    {
        Tick = false;
    }
}
