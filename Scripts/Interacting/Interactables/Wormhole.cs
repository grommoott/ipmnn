using UnityEngine;
using System.Collections.Generic;
using Saves;


namespace Interacting.Interactables
{
    public class Wormhole : MonoBehaviour, IInteractable
    {
        [SerializeField] private Location _location;
        [SerializeField] private string _destinationName;

        public List<Interaction> GetInteractions()
        {
            return new() {
                new Interaction
                    (
                        "Войти в " + _destinationName,
                        InteractingMethod.DefaultInteract,
                        (interactor) => {
                            SavesManager.GoToScene(LocationNames.GetFromEnum(_location));
                        },
                        (interactor) => {
                            return interactor.GetInteractorType() == InteractorType.Player;
                        }
                    )
            };
        }
    }
}
