using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorSubscriber : MonoBehaviour
{
    private Subscription<OpenDoorEventType> subscription;

    private void Start()
    {
        subscription = EventAggregator.SingleInstance.Subscribe<OpenDoorEventType>(OpenDoor);
    }

    private void OpenDoor(OpenDoorEventType openDoorEvent)
    {
        openDoorEvent.DoorRb.gravityScale = openDoorEvent.GravityChange;
    }
}
