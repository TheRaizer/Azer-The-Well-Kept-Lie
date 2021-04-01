using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscription<T>
{
    public Action<T> Action { get; private set; }

    private readonly EventAggregator eventAggregator;

    public Subscription(Action<T> action, EventAggregator _eventAggregator)
    {
        Action = action;
        eventAggregator = _eventAggregator;
    }

    public void Dispose()
    {
        eventAggregator.Unsubscribe(this);
    }
}
