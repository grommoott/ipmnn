using UnityEngine;
using System.Collections.Generic;
using Interacting;
using Player;

namespace Items.Shell
{
    public class ItemShell : MonoBehaviour, IInteractable
    {
        private Item _item = null;

        private List<Interaction> _interactions = new();

        private void Awake()
        {
            _interactions.Add(new Interaction("Поднять", InteractingMethod.DefaultInteract, (interactor) =>
            {
                switch (interactor.GetInteractorType())
                {
                    case InteractorType.Player:
                        PlayerController player = interactor as PlayerController;

                        _item = player.InventoryManager.AddItemWithRemainder(_item);

                        if (_item == null)
                        {
                            Destroy(gameObject);
                            return;
                        }

                        return;

                    default:
                        return;

                }
            }, (interactor) =>
            {
                return true;
            }));
        }

        public Item GetCount(int count)
        {
            Item item = _item.GetCount(count);

            if (_item.Count == 0)
            {
                Destroy(gameObject);
            }

            return item;
        }

        public Item AddCount(int count)
        {
            Item item = _item.AddCount(count);
            return item;
        }

        public void SetItem(Item item)
        {
            if (_item != null)
            {
                Debug.LogAssertion("Trying to set Item to ItemShell that already contains Item");
                return;
            }
            _item = item;

            GameObject model = Resources.Load<GameObject>("Models/Items/" + item.Id);
            Instantiate(model, transform);
        }

        public List<Interaction> GetInteractions()
        {
            return _interactions;
        }
    }
}
