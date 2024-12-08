using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quests;
using Player;
using System;

namespace UI.Pages.Quests
{
    public class QuestElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _questNameText;
        [SerializeField] private TMP_Text _questDescriptionText;
        [SerializeField] private TMP_Text _toggleSelectQuestButtonText;
        [SerializeField] private Button _questButton;
        [SerializeField] private string _activeQuestButtonText;
        [SerializeField] private string _inactiveQuestButtonText;
        [SerializeField] private string _finishedQuestButtonText;
        [SerializeField] private string _notStartedQuestButtonText;

        private Quest _quest;

        private void Start()
        {
            if (_quest == null)
            {
                return;
            }

            string questTasks = "";

            if (_quest.State == QuestState.Finished)
            {
                for (int i = 0; i < _quest.Tasks.Count; i++)
                {
                    questTasks += $"- {_quest.Tasks[i].Description} : Выполнено";
                    questTasks += "\n";
                }
            }
            else
            {
                for (int i = 0; i < _quest.Tasks.Count; i++)
                {
                    if (_quest.ActiveTaskIndex < i)
                    {
                        questTasks += $"- {_quest.Tasks[i].Description} : Выполнено";
                    }
                    else if (_quest.ActiveTaskIndex == i)
                    {
                        questTasks += $"- {_quest.Tasks[i].Description} : {_quest.Tasks[i].GetProgress()}";
                    }
                    else
                    {
                        questTasks += $"- {_quest.Tasks[i].Description}";
                    }

                    questTasks += "\n";
                }
            }

            _questNameText.text = _quest.Name;
            _questDescriptionText.text = $"{_quest.Description}\n\nЗадачи:\n{questTasks}";

            bool isQuestSelected = PlayerController.Instance.QuestManager.ActiveQuestId == _quest.Id;

            _questButton.onClick.AddListener(() =>
                {
                    switch (_quest.State)
                    {
                        case QuestState.Finished:
                            PlayerController.Instance.QuestManager.ClaimReward(_quest.Id);
                            break;

                        case QuestState.Active:
                            PlayerController.Instance.QuestManager.SelectNone();
                            break;

                        case QuestState.Inactive:
                            PlayerController.Instance.QuestManager.SelectQuestById(_quest.Id);
                            break;

                        case QuestState.NotStarted:
                            PlayerController.Instance.QuestManager.SelectQuestById(_quest.Id);
                            break;
                    }

                    UpdateElement();
                });

            UpdateElement();
        }

        private void UpdateElement()
        {
            if (_quest == null)
            {
                return;
            }

            switch (_quest.State)
            {
                case QuestState.Finished:
                    _toggleSelectQuestButtonText.text = _finishedQuestButtonText;
                    break;

                case QuestState.Active:
                    _toggleSelectQuestButtonText.text = _activeQuestButtonText;
                    break;

                case QuestState.Inactive:
                    _toggleSelectQuestButtonText.text = _inactiveQuestButtonText;
                    break;

                case QuestState.NotStarted:
                    _toggleSelectQuestButtonText.text = _notStartedQuestButtonText;
                    break;
            }
        }

        public void SetQuest(Quest quest)
        {
            _quest = quest;
        }

        public void SetOnClick(Action onClick)
        {
            _questButton.onClick.AddListener(() => onClick.Invoke());
        }
    }
}
