using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Interacting.Interactables
{
    public class Bed : MonoBehaviour, IInteractable
    {
        public List<Interaction> GetInteractions()
        {
            return new()
            {
                new Interaction
                    (
                    "Лечь спать",
                    InteractingMethod.DefaultInteract,
                    OnInteraction,
                    GetIsInteractionAvailable
                    )
            };
        }

        private void OnInteraction(IInteractor interactor)
        {
            if (!GetIsInteractionAvailable(interactor))
            {
                return;
            }

            Player.PlayerController.Instance.SleepingManager.Sleep();
        }

        private bool GetIsInteractionAvailable(IInteractor interactor)
        {
            if (interactor.GetInteractorType() != InteractorType.Player)
            {
                return false;
            }

            return PlayerController.Instance.StateManager.Stamine <=
                Player.PlayerController.Instance.SleepingManager.MaxStamineToSleep;
        }
    }
}
