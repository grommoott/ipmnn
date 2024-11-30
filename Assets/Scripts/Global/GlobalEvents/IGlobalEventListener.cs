using System;

namespace Global.GlobalEvents
{
    public interface IGlobalEventListener<T>
    {
        public Action<T> GetCallback();
        public void OnDestroy();
    }

    public interface IGlobalEventListener
    {
        public Action GetCallback();
        public void OnDestroy();
    }
}
