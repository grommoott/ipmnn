using Global.GlobalEvents.GlobalEventLists;

namespace Global.GlobalEvents
{
    public class GlobalEventsManager
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

        public readonly Input Input = new Input();
        public readonly Game Game = new Game();
        public readonly GlobalEventLists.Player Player = new GlobalEventLists.Player();
    }
}

