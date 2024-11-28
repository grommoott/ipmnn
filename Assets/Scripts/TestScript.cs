using UnityEngine;
using System.Collections.Generic;
using System;

public class GlobalEvent<T>
{
    private Action<T> _event;

    public void Invoke(T arg)
    {
        _event.Invoke(arg);
    }

    public void Subscribe(Action<T> function)
    {
        _event += function;
    }

    public void Unsubscribe(Action<T> function)
    {
        _event -= function;
    }
}


public class GlobalEvent
{
    private Action _event;

    public void Invoke()
    {
        _event.Invoke();
    }

    public void Subscribe(Action function)
    {
        _event += function;
    }

    public void Unsubscribe(Action function)
    {
        _event -= function;
    }
}

public class WarGlobalEvents
{
    public static GlobalEvent OreshnikVzorvalsya = new GlobalEvent();
}

public class GlobalEventsManager : WarGlobalEvents
{
    private static GlobalEventsManager _instance;

    public GlobalEventsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GlobalEventsManager();
            }

            return _instance;
        }
    }

    public GlobalEvent MeteoritUpal = new GlobalEvent();
    public GlobalEvent CorablPriletel = new GlobalEvent();
}

/*interface IGlobalEventListener*/
/*{*/
/*    public void OnDestroy();*/
/*}*/
