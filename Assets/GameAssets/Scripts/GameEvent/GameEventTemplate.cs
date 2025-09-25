using System;
using UnityEngine;

public class GameEvent
{
    public event Action Event;

    public void RegisterEvent(Action eventAction)
    {
        if (eventAction != null)
        {
            Event += eventAction;
        }
    }

    public void UnregisterEvent(Action eventAction)
    {
        if (eventAction != null)
        {
            Event -= eventAction;
        }
    }

    public void RaiseEvent()
    {
        Event?.Invoke();
    }

    public void UnRegisterAll()
    {
        Event = null;
    }
}

public class GameEvent<T>
{
    public event Action<T> Event;
    public void RegisterEvent(Action<T> eventAction)
    {
        if (eventAction != null)
        {
            Event += eventAction;
        }
    }

    public void UnregisterEvent(Action<T> eventAction)
    {
        if (eventAction != null)
        {
            Event -= eventAction;
        }
    }

    public void RaiseEvent(T param)
    {
        Event?.Invoke(param);
    }

    public void UnRegisterAll()
    {
        Event = null;
    }
}

public class GameEvent<T1,T2>
{
    public event Action<T1,T2> Event;
    public void RegisterEvent(Action<T1,T2> eventAction)
    {
        if (eventAction != null)
        {
            Event += eventAction;
        }
    }

    public void UnregisterEvent(Action<T1,T2> eventAction)
    {
        if (eventAction != null)
        {
            Event -= eventAction;
        }
    }

    public void RaiseEvent(T1 param1, T2 param2)
    {
        Event?.Invoke(param1, param2);
    }

    public void UnRegisterAll()
    {
        Event = null;
    }
}

public class GameEvent<T1, T2,T3>
{
    public event Action<T1, T2,T3> Event;
    public void RegisterEvent(Action<T1, T2,T3> eventAction)
    {
        if (eventAction != null)
        {
            Event += eventAction;
        }
    }

    public void UnregisterEvent(Action<T1, T2,T3> eventAction)
    {
        if (eventAction != null)
        {
            Event -= eventAction;
        }
    }

    public void RaiseEvent(T1 param1, T2 param2, T3 param3)
    {
        Event?.Invoke(param1, param2, param3);
    }

    public void UnRegisterAll()
    {
        Event = null;
    }
}
