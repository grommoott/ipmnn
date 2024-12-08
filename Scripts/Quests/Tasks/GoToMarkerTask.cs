using UnityEngine;
using Global.GlobalEvents;
using Helpers;

namespace Quests.Tasks
{
    public class GoToMarkerTask : QuestTask
    {
        private Vector3 _position;
        private float _completionRadius;
        private string _labelText;
        private Marker _marker;

        private float _distance
        {
            get
            {
                return Vector3.Distance(_position, Player.PlayerController.Instance.transform.position);
            }
        }

        override public string GetProgress()
        {
            return $"{_distance.ToString("0.0")} метров";
        }

        override protected void OnInitialize()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Subscribe(OnUpdate, this);
        }

        private void OnUpdate()
        {
            if (_distance <= _completionRadius)
            {
                OnComplete();
            }
        }

        override public void ShowHints()
        {
            _marker = Marker.Spawn(_position, _labelText);
        }

        override public void HideHints()
        {
            _marker.Destroy();
        }

        override public void OnDestroy()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Unsubscribe(OnUpdate, this);
        }

        public GoToMarkerTask(string description, Vector3 position, string labelText = "", float completionRadius = 10) : base(description)
        {
            _labelText = labelText;
            _position = position;
            _completionRadius = completionRadius;
        }
    }
}
