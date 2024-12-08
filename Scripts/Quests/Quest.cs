using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

namespace Quests
{
    public class Quest
    {
        private string _id;
        public string Id { get { return _id; } }

        private string _name;
        public string Name { get { return _name; } }

        private string _description;
        public string Description { get { return _description; } }

        private QuestState _state = QuestState.NotStarted;
        public QuestState State { get { return _state; } }

        private QuestRewards _rewards;
        public QuestRewards Rewards { get { return _rewards; } }

        private List<QuestTask> _tasks;
        public ReadOnlyCollection<QuestTask> Tasks { get { return _tasks.AsReadOnly(); } }

        private int _activeTaskIndex = 0;

        public int ActiveTaskIndex { get { return _activeTaskIndex; } }
        public QuestTask ActiveTask { get { return _tasks[_activeTaskIndex]; } }

        public Action<string> OnFinish;

        private void NextTask()
        {
            _tasks[_activeTaskIndex].Deselect();
            _activeTaskIndex++;

            if (_activeTaskIndex >= _tasks.Count)
            {
                _state = QuestState.Finished;
                _activeTaskIndex--;
                OnFinish.Invoke(Id);
            }
            else
            {
                _tasks[_activeTaskIndex].Select();
            }
        }

        public void Select()
        {
            switch (_state)
            {
                case QuestState.NotStarted:
                    ActiveTask.Select();
                    _state = QuestState.Active;
                    break;

                case QuestState.Inactive:
                    ActiveTask.ShowHints();
                    _state = QuestState.Active;
                    break;

                default:
                    Debug.LogWarning("Trying to select active or finished quest");
                    break;
            }
        }

        public void Deselect()
        {
            ActiveTask.HideHints();

            switch (_state)
            {
                case QuestState.Active:
                    _state = QuestState.Inactive;
                    break;

                default:
                    Debug.LogWarning("Trying to deselect not started, inactive or finished quest");
                    break;
            }
        }

        public void GiveRewards()
        {
            _rewards.GiveRewards();
        }

        public Quest
            (
                string id,
                string name,
                string description,
                QuestState state,
                QuestRewards rewards,
                List<QuestTask> tasks,
                int activeTaskId
            )
        {
            _id = id;
            _name = name;
            _description = description;
            _state = state;
            _rewards = rewards;
            _tasks = tasks;
            _activeTaskIndex = activeTaskId;

            foreach (QuestTask task in tasks)
            {
                task.OnComplete = NextTask;
            }
        }
    }
}
