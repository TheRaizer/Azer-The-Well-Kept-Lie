using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorStopBarrierOnPressurePlate : MonoBehaviour
{
    [SerializeField] private OpenDoorEventType doorEventType = null;
    [SerializeField] private DisableGameObjectEventType disableObjectType = null;

    public bool isPressed = false;

    private float doorEventTypeOpenSpeed = 0;

    private void Awake()
    {
        doorEventTypeOpenSpeed = doorEventType.GravityChange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isPressed)
        {
            isPressed = true;
            if (doorEventType != null)
            {
                doorEventType.GravityChange = doorEventTypeOpenSpeed;
                EventAggregator.SingleInstance.Publish(doorEventType);
            }
            if(disableObjectType != null)
                EventAggregator.SingleInstance.Publish(disableObjectType);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isPressed)
        {
            isPressed = false;
            if (doorEventType != null)
            {
                doorEventType.GravityChange = 1;
                EventAggregator.SingleInstance.Publish(doorEventType);
            }
        }
    }
}
