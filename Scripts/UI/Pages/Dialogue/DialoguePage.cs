using UnityEngine;
using TMPro;

namespace UI.Pages.Dialogue
{
    public class DialoguePage : Page
    {
        [SerializeField] private TMP_Text _companionNameText;
        [SerializeField] private TMP_Text _dialogueReplicText;

        private int _replicIndex;
        private Helpers.Dialogue _dialogue;

        private void Start()
        {
            UpdatePage();
        }

        public void UpdatePage()
        {
            if (_dialogue == null)
            {
                Debug.LogWarning("Dialogue is null");
                return;
            }

            _companionNameText.text = _dialogue.CompanionName;

            if (_dialogue.Replics.Count <= _replicIndex)
            {
                Debug.LogWarning("Replic index is out of range");
                return;
            }

            _dialogueReplicText.text = _dialogue.Replics[_replicIndex];
        }

        public void Next()
        {
            _replicIndex++;

            if (_replicIndex >= _dialogue.Replics.Count)
            {
                _dialogue.OnDialogueEnd();
                UIManager.Instance.ClosePage();
                return;
            }

            UpdatePage();
        }

        public void SetDialogue(Helpers.Dialogue dialogue)
        {
            _dialogue = dialogue;
        }

        public static DialoguePage SpawnPage(Helpers.Dialogue dialogue)
        {
            GameObject pageGO = UIManager.Instance.InstantiatePage(UIManager.Instance.DialoguePage);
            DialoguePage page = pageGO.GetComponent<DialoguePage>();
            page.SetDialogue(dialogue);

            return page;
        }
    }
}
