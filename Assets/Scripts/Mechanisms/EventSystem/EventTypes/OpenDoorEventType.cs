using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorEventType : MonoBehaviour
{
    [field: SerializeField] public float GravityChange { get; set; } = 0f;
    [field: SerializeField] public Rigidbody2D DoorRb { get; set; } = null;
}
