using System;

namespace Global.GlobalEvents
{
    public class GlobalEvent
    {
        private Action _event;

        public void Invoke()
        {
            if (_event == null)
            {
                return;
            }
            _event.Invoke();
        }

        public bool Subscribe(Action callback, IGlobalEventListener listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event += callback;
            return true;
        }

        public bool Unsubscribe(Action callback, IGlobalEventListener listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event -= callback;
            return true;
        }
    }

    public class GlobalEvent<T>
    {
        private Action<T> _event;

        public void Invoke(T argument)
        {
            if (_event == null)
            {
                return;
            }
            _event.Invoke(argument);
        }

        public bool Subscribe(Action<T> callback, IGlobalEventListener listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event += callback;
            return true;
        }

        public bool Unsubscribe(Action<T> callback, IGlobalEventListener listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event -= callback;
            return true;
        }
    }
}
