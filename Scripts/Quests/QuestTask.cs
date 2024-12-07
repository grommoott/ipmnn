using System;
using Global.GlobalEvents;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Quests
{
    public class QuestTask : IGlobalEventListener
    {
        private string _description;
        public string Description { get { return _description; } }

        protected virtual void OnInitialize() { }

        private bool _isActive = false;
        public bool IsActive { get { return _isActive; } }

        public virtual void ShowHints()
        {
            _isActive = true;
        }

        public virtual void HideHints()
        {
            _isActive = false;
        }

        public virtual string GetProgress()
        {
            return "";
        }

        public virtual void OnDestroy() { }

        public Action OnComplete;

        public void Select()
        {
            OnInitialize();
            ShowHints();
        }

        public void Deselect()
        {
            OnDestroy();
            HideHints();
        }

        public virtual JObject GetState()
        {
            return null;
        }

        public virtual void SetState(JObject state)
        {
            if (state == null)
            {
                return;
            }

            return;
        }

        public QuestTask(string description)
        {
            _description = description;
        }
    }

    public class QuestTaskWithState<T> : QuestTask
    {
        public T State;

        public override JObject GetState()
        {
            return JObject.FromObject(State);
        }

        public override void SetState(JObject state)
        {
            if (state == null)
            {
                return;
            }

            State = state.ToObject<T>();
        }

        public QuestTaskWithState
            (
                string description
            ) : base(description)
        { }
    }
}
