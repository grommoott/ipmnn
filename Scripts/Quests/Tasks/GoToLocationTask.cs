using Global.GlobalEvents;

namespace Quests.Tasks
{
    public class GoToLocationTask : QuestTask
    {
        private string _locationName;

        override public string GetProgress() => "В процессе...";

        override protected void OnInitialize()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Subscribe(OnUpdate, this);
        }

        private void OnUpdate()
        {
            if (!IsActive)
            {
                return;
            }

            if (Saves.SavesManager.CurrentLocationName == _locationName)
            {
                OnComplete();
            }
        }

        override public void OnDestroy()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Unsubscribe(OnUpdate, this);
        }

        public GoToLocationTask(string locationName, string description) : base(description)
        {
            _locationName = locationName;
        }
    }
}
