using System.Collections.Generic;
using UnityEngine;
using Global.GlobalEvents;
using System;
using Player;
using Effects;
using Newtonsoft.Json.Linq;

namespace Quests.List
{
    public static class TestQuests
    {
        public static readonly Dictionary<string, Func<QuestState, int, JObject, Quest>> Quests = new()
        {
            { QuestIds.FirstSteps, FirstSteps }
        };

        [Serializable]
        private struct TaskaState
        {
            public float Distance;
            [Newtonsoft.Json.JsonProperty("LastPosition")] private Serialization.Vector _lastPosition;
            [Newtonsoft.Json.JsonIgnore] public Vector3 LastPosition { get { return _lastPosition; } set { _lastPosition = value; } }

            [Newtonsoft.Json.JsonConstructor]
            public TaskaState(float distance, Serialization.Vector lastPosition)
            {
                _lastPosition = null;
                Distance = distance;
                if (lastPosition != null)
                {
                    _lastPosition = lastPosition;
                }
            }
        }

        private class Taska : QuestTaskWithState<TaskaState>
        {
            override public string GetProgress()
            {
                return $"Пройдено {State.Distance.ToString("0.0")}m из 100m";
            }

            override protected void OnInitialize()
            {
                State.LastPosition = PlayerController.Instance.transform.position;

                GlobalEventsManager.Instance.Game.OnUpdate.Subscribe(OnUpdate, this);
            }

            override public void OnDestroy()
            {
                GlobalEventsManager.Instance.Game.OnUpdate.Unsubscribe(OnUpdate, this);
            }

            private void OnUpdate()
            {
                if (!IsActive)
                {
                    return;
                }

                State.Distance += Vector3.Distance(State.LastPosition, PlayerController.Instance.transform.position);
                State.LastPosition = PlayerController.Instance.transform.position;

                if (State.Distance > 100)
                {
                    OnComplete.Invoke();
                }
            }

            public Taska(bool isCompleted)
                : base("Идём...")
            { }
        }

        private static Quest FirstSteps(QuestState state, int activeTaskIndex, JObject activeTaskState)
        {
            Taska task = new Taska(false);

            List<QuestTask> tasks = new List<QuestTask>() { task };

            tasks[activeTaskIndex].SetState(activeTaskState);

            Quest quest = new Quest
                (
                    QuestIds.FirstSteps,
                    "Первые шаги",
                    "Сделайте первые шаги",
                    QuestState.NotStarted,
                    new QuestRewards(new(), null,
                        () =>
                        {
                            PlayerController.Instance.EffectsManager
                            .AddTemporaryEffect(EffectsManager.GetTemporary(EffectIds.Speed, 5, 10));
                        }),
                    new List<QuestTask>() { task },
                    activeTaskIndex
                );

            return quest;
        }
    }

}
