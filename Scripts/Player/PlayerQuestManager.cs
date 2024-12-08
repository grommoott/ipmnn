using UnityEngine;
using Quests;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using Saves;
using Global.GlobalEvents;

namespace Player
{
    public class PlayerQuestManager : MonoBehaviour, ISaveable
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        private List<string> _completedQuestIds;
        public ReadOnlyCollection<string> CompletedQuestIds { get { return _completedQuestIds.AsReadOnly(); } }

        private Dictionary<string, Quest> _quests;
        public ReadOnlyDictionary<string, Quest> Quests { get { return new(_quests); } }

        private string _activeQuestId = "";

        public string ActiveQuestId { get { return _activeQuestId; } }

        public Quest ActiveQuest
        {
            get
            {
                if (_activeQuestId == "")
                {
                    return null;
                }

                return _quests.GetValueOrDefault(_activeQuestId, null);
            }
        }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
            _quests = new();
            _completedQuestIds = new();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);
        }

        private void OnQuestFinish(string id)
        {
            if (_activeQuestId == id)
            {
                _activeQuestId = "";
            }

            _completedQuestIds.Add(id);
            GlobalEventsManager.Instance.Player.OnQuestComplete.Invoke();
        }

        public void SelectNone()
        {
            if (_activeQuestId != "")
            {
                ActiveQuest.Deselect();
            }

            _activeQuestId = "";
            GlobalEventsManager.Instance.Player.OnActiveQuestChange.Invoke();
        }

        public void SelectQuestById(string id)
        {
            if (_completedQuestIds.Contains(id))
            {
                Debug.LogWarning("Trying to select finished quest");
                return;
            }

            if (_activeQuestId != "")
            {
                ActiveQuest.Deselect();
            }
            Quest activeQuest = _quests.GetValueOrDefault(id, null);

            if (activeQuest == null || activeQuest.State == QuestState.Finished)
            {
                return;
            }

            _activeQuestId = activeQuest.Id;
            ActiveQuest.Select();
            GlobalEventsManager.Instance.Player.OnActiveQuestChange.Invoke();
        }

        public void AddQuest(Quest quest)
        {
            if (_completedQuestIds.Contains(quest.Id) && quest.State != QuestState.Finished)
            {
                Debug.LogWarning("Player has already completed quest \"" + quest.Name + "\"");
                return;
            }

            if (_quests.ContainsKey(quest.Id))
            {
                Debug.LogWarning("Player has already have this quest");
                return;
            }

            _quests.Add(quest.Id, quest);
            quest.OnFinish = OnQuestFinish;

            if (quest.State == QuestState.Inactive)
            {
                SelectQuestById(quest.Id);
            }

            GlobalEventsManager.Instance.Player.OnQuestListChange.Invoke();
        }

        public void ClaimReward(string id)
        {
            Quest quest = _quests.GetValueOrDefault(id, null);

            if (quest == null || quest.State != QuestState.Finished)
            {
                return;
            }

            quest.GiveRewards();
            _quests.Remove(id);
            GlobalEventsManager.Instance.Player.OnQuestListChange.Invoke();
        }

        public void Load(Newtonsoft.Json.Linq.JObject data)
        {
            if (data == null)
            {
                return;
            }

            Serialization.PlayerQuests quests = data.ToObject<Serialization.PlayerQuests>();

            _completedQuestIds = quests.CompletedQuestIds;

            foreach (Serialization.Quest quest in quests.Quests)
            {
                AddQuest(QuestsManager
                        .GetById(quest.State, quest.Id, quest.ActiveTaskIndex, quest.ActiveTask.State));
            }

            SelectNone();

            if (_completedQuestIds.Contains(quests.ActiveQuestId) || quests.ActiveQuestId == "")
            {
                return;
            }

            SelectQuestById(quests.ActiveQuestId);
        }

        public object Save()
        {
            return new Serialization.PlayerQuests(_quests, _completedQuestIds, _activeQuestId);
        }

        public string GetSavingPath() => "playerQuests";
    }
}
