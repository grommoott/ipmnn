using UnityEngine;
using Quests;
using TMPro;
using UnityEngine.UI;
using System;

namespace UI.Pages.Quests
{
    public class ListQuestElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _questNameText;
        [SerializeField] private Button _selectQuestButton;
        private Quest _quest;
        private Action<string> _onClick;

        private void Start()
        {
            _selectQuestButton.onClick.AddListener(() =>
            {
                if (_onClick == null)
                {
                    return;
                }
                _onClick.Invoke(_quest.Id);
            });
        }

        public void SetQuest(Quest quest)
        {
            _quest = quest;
            _questNameText.text = _quest.Name;
        }

        public void SetOnSelect(Action<string> callback)
        {
            _onClick = callback;
        }
    }
}
