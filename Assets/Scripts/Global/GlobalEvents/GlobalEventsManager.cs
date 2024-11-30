namespace Global.GlobalEvents
{
    public class GlobalEventsManager : GlobalEventLists.Input
    {
        private static GlobalEventsManager _instance = null;
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

