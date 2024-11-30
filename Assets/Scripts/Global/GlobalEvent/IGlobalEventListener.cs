using System;

namespace Global.GlobalEvent
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
