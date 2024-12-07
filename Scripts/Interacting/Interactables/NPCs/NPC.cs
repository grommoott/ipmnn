using UnityEngine;
using System.Collections.Generic;
using UI;
using Helpers;

namespace Interacting.Interactables.NPSs
{
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        [SerializeField] private NameText _nameText;

        private void Start()
        {
            NameText nameText = Instantiate(_nameText, transform);
            nameText.SetText(GetName());
        }

        public abstract Dialogue GetDialogue();

        public abstract string GetName();

        public List<Interaction> GetInteractions()
        {
            return new()
            {
                new Interaction
                (
                    "Поговорить",
                    InteractingMethod.DefaultInteract,
                    (interactor) =>
                    {
                        UIManager.Instance.OpenPage(UI.Pages.Dialogue.DialoguePage.SpawnPage(GetDialogue()));
                    },
                    (interactor) =>
                    {
                        return interactor.GetInteractorType() == InteractorType.Player;
                    }
                )
            };
        }
    }
}
