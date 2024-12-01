using UnityEngine;
using Interacting;
using Global.InputManager;
using System.Collections.Generic;

namespace Player
{
    public class PlayerInteractingManager : MonoBehaviour
    {
        private PlayerController _player;
        public PlayerController Player
        {
            get
            {
                return _player;
            }
        }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        private void Update()
        {
            IInteractable interactable = GetNearestInteractableOnRay();

            if (interactable == null)
            {
                return;
            }


            // Test

            if (InputManager.Instance.IsInteract)
            {
                List<Interaction> interactings = interactable.GetInteractions();
                Interaction interaction = interactings.Find((interaction) => interaction.Method == InteractingMethod.DefaultInteract);

                if (interaction == null)
                {
                    return;
                }

                interaction.OnInteract.Invoke(_player);
            }
        }

        private IInteractable GetNearestInteractableOnRay()
        {
            Ray ray = Player.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit[] hits = Physics.RaycastAll(ray);

            IInteractable nearest = null;
            float distanceToNearest = Mathf.Infinity;

            foreach (RaycastHit hit in hits)
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                if (distance < distanceToNearest)
                {
                    IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                    if (interactable == null)
                    {
                        continue;
                    }

                    nearest = interactable;
                    distanceToNearest = distance;
                }
            }

            return nearest;
        }
    }
}
