using UnityEngine;
using Player;
using System.Collections.Generic;
using Quests;
using System.Linq;
using Global.GlobalEvents;

namespace UI.Pages.Quests
{
    public class QuestsPage : Page, IGlobalEventListener
    {
        [SerializeField] private GameObject _listQuestElement;
        [SerializeField] private GameObject _questElement;
        [SerializeField] private Transform _scrollQuestsContainer;
        [SerializeField] private Transform _activeQuestContainer;
        [SerializeField] private Transform _openedQuestContainter;
        [SerializeField] private float _listQuestOffset;
        [SerializeField] private float _listQuestInitialOffset;
        private Quest _openedQuest;

        private void Start()
        {
            _openedQuest = PlayerController.Instance.QuestManager.ActiveQuest;

            if (_openedQuest == null && PlayerController.Instance.QuestManager.Quests.Count != 0)
            {
                _openedQuest = PlayerController.Instance.QuestManager.Quests.Values.First();
            }

            GlobalEventsManager.Instance.Player.OnQuestListChange.Subscribe(UpdateList, this);
            GlobalEventsManager.Instance.Player.OnActiveQuestChange.Subscribe(UpdateList, this);
            GlobalEventsManager.Instance.Player.OnQuestComplete.Subscribe(UpdateAll, this);

            UpdateList();
            UpdateOpened();
        }

        private void UpdateAll()
        {
            UpdateOpened();
            UpdateList();
        }

        private void UpdateOpened()
        {
            ClearChildren(_openedQuestContainter);

            if (_openedQuest == null)
            {
                return;
            }

            QuestElement element =
                Instantiate(_questElement, _openedQuestContainter)
                .GetComponent<QuestElement>();

            element.SetOnClick(UpdateAll);
            element.SetQuest(_openedQuest);
        }

        private void UpdateList()
        {
            ClearChildren(_scrollQuestsContainer);
            ClearChildren(_activeQuestContainer);

            string activeQuestId = PlayerController.Instance.QuestManager.ActiveQuestId;
            int questsCount = PlayerController.Instance.QuestManager.Quests.Count;

            int i = 0;
            foreach (KeyValuePair<string, Quest> pair in PlayerController.Instance.QuestManager.Quests)
            {
                if (pair.Key == activeQuestId)
                {
                    continue;
                }

                ListQuestElement element =
                    Instantiate(_listQuestElement, _scrollQuestsContainer)
                    .GetComponent<ListQuestElement>();

                element.SetQuest(pair.Value);
                element.SetOnSelect(OpenQuest);
                element.GetComponent<RectTransform>().Translate(new Vector3(0, -_listQuestOffset * i - _listQuestInitialOffset, 0));
                i++;
            }

            if (activeQuestId != "")
            {
                ListQuestElement element =
                    Instantiate(_listQuestElement, _activeQuestContainer)
                    .GetComponent<ListQuestElement>();

                element.SetQuest(PlayerController.Instance.QuestManager.ActiveQuest);
                element.SetOnSelect(OpenQuest);
            }
        }

        private void ClearChildren(Transform parent)
        {
            if (parent == null)
            {
                return;
            }

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
        }

        public void OpenQuest(string id)
        {
            _openedQuest = PlayerController.Instance.QuestManager.Quests[id];
            UpdateAll();
        }

        public static QuestsPage SpawnPage()
        {
            return UIManager.Instance.InstantiatePage(UIManager.Instance.QuestsPage).GetComponent<QuestsPage>();
        }

        public void OnDestroy()
        {
            GlobalEventsManager.Instance.Player.OnQuestListChange.Unsubscribe(UpdateList, this);
            GlobalEventsManager.Instance.Player.OnActiveQuestChange.Unsubscribe(UpdateList, this);
            GlobalEventsManager.Instance.Player.OnQuestComplete.Subscribe(UpdateAll, this);
        }
    }
}
