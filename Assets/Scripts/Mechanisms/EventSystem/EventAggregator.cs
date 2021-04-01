using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventAggregator : MonoBehaviour
{
    private readonly Dictionary<Type, IList> subscribers = new Dictionary<Type, IList>();

    public static EventAggregator SingleInstance { get; private set; }


    private void Awake()
    {
        if(SingleInstance == null)
        {
            SingleInstance = this;
        }
    }




    public void Publish<T>(T message)
    {
        IList actionList;

        if (subscribers.ContainsKey(typeof(T)))
        {
            actionList = new List<Subscription<T>>(subscribers[typeof(T)].Cast<Subscription<T>>());

            if (actionList.Count > 0)
            {
                foreach (Subscription<T> sub in actionList)
                {
                    sub.Action?.Invoke(message);
                }
            }
            else
                Debug.Log("No Sub Found");
        }
        else
            throw new NullReferenceException();
    }



    public Subscription<T> Subscribe<T>(Action<T> action)
    {
        Subscription<T> subscription = new Subscription<T>(action, SingleInstance);

        if(subscribers.TryGetValue(typeof(T), out IList actionList))
        {
            actionList.Add(subscription);
        }
        else
        {
            actionList = new List<Subscription<T>> { subscription };
            subscribers.Add(typeof(T), actionList);
        }

        return subscription;
    }



    public void Unsubscribe<T>(Subscription<T> subscription)
    {
        if (subscribers[typeof(T)].Contains(subscription))
        {
            int indexToRemove = subscribers[typeof(T)].IndexOf(subscription);

            subscribers[typeof(T)].RemoveAt(indexToRemove);
        }
    }


    public void ClearAllSubscriptions()
    {
        subscribers.Clear();
    }
}
