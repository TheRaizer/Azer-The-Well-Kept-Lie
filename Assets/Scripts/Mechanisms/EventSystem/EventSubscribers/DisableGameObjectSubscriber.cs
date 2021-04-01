using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectSubscriber : MonoBehaviour
{
    private Subscription<DisableGameObjectEventType> subscription;

    private void Start()
    {
        subscription = EventAggregator.SingleInstance.Subscribe<DisableGameObjectEventType>(DisableGameObject);
    }

    private void DisableGameObject(DisableGameObjectEventType disableEventType)
    {
        disableEventType.GameObjectToDisable.SetActive(false);
    }
}
