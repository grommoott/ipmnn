using Global.GlobalEvent.GlobalEventLists;

namespace Global.GlobalEvent
{
    public class GlobalEventsManager : Input
    {
        private static GlobalEventsManager _instance;
        public static GlobalEventsManager Instance
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
    }
}

