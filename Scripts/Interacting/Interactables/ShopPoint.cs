using UnityEngine;
using System.Collections.Generic;
using Shops;
using UI;
using Helpers;
using System.Collections;

namespace Interacting.Interactables
{
    public class ShopPoint : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _shopId;
        [SerializeField] private NameText _nameText;
        private Shop _shop;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => Saves.SavesManager.Instance.IsGameLoaded);
            _nameText.SetText("Магазин");
            _shop = ShopsManager.Instance.GetById(_shopId);
        }

        public List<Interaction> GetInteractions()
        {
            return new()
            {
                new Interaction
                (
                    "Магазин",
                    InteractingMethod.DefaultInteract,
                    (_) =>
                    {
                        UIManager.Instance.OpenPage(UI.Pages.Shop.ShopPage.SpawnPage(_shop));
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
