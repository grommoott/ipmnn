using System;

namespace Global.GlobalEvent
{
    public class GlobalEvent
    {
        private Action _event;

        public void Invoke()
        {
            _event.Invoke();
        }

        public bool Subscribe(IGlobalEventListener listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event += listener.GetCallback();
            return true;
        }

        public bool Unsubscribe(IGlobalEventListener listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event -= listener.GetCallback();
            return true;
        }
    }

    public class GlobalEvent<T>
    {
        private Action<T> _event;

        public void Invoke(T argument)
        {
            _event.Invoke(argument);
        }

        public bool Subscribe(IGlobalEventListener<T> listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event += listener.GetCallback();
            return true;
        }

        public bool Unsubscribe(IGlobalEventListener<T> listener)
        {
            if (listener == null)
            {
                return false;
            }
            _event -= listener.GetCallback();
            return true;
        }
    }
}
