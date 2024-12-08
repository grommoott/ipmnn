using System.Collections.Generic;
using Player;
using Items;
using Items.Classes.InteractingTools;
using Items.Storages;
using UnityEngine;

namespace Interacting.Interactables
{
    public class Flower : MonoBehaviour, IInteractable
    {
        public List<Interaction> GetInteractions()
        {
            return new()
            {
                new Interaction
                    (
                        "Поймать пчелу",
                        InteractingMethod.LMBInteract,
                        (interactor) =>
                        {
                            Item inHand =
                            PlayerController.Instance.InventoryManager
                            .Inventory.PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Hand, null);

                            if (inHand == null)
                            {
                                return;
                            }
                            (inHand as InteractingTool).InteractingToolUse(() => OnLMBInteraction(interactor));
                        },
                        GetIsLMBInteractionAvailable

                    )
            };
        }

        public void OnLMBInteraction(IInteractor interactor)
        {
            if (interactor.GetInteractorType() != InteractorType.Player)
            {
                return;
            }

            Item bee = ItemManager.GetById(ItemIds.Bee, 1);
            PlayerController.Instance.InventoryManager.AddItem(bee);
        }

        public bool GetIsLMBInteractionAvailable(IInteractor interactor)
        {
            if (interactor.GetInteractorType() != InteractorType.Player)
            {
                return false;
            }

            Item inHand =
                PlayerController.Instance.InventoryManager
                .Inventory.PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Hand, null);

            if (inHand == null)
            {
                return false;
            }

            return inHand.Id == ItemIds.Net && !(inHand as InteractingTool).IsBusy;
        }
    }
}
