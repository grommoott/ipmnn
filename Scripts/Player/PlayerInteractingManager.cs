using UnityEngine;
using Interacting;
using Global.InputManager;
using System.Collections.Generic;
using System.Collections;
using Saves;
using UI;

namespace Player
{
    public class PlayerInteractingManager : MonoBehaviour
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        [SerializeField] private float _maxInteractionDistance;

        private Interaction _onDefaultInteraction;
        public Interaction OnDefaultInteraction { get { return _onDefaultInteraction; } }

        private Interaction _onLMBInteraction;
        public Interaction OnLMBInteraction { get { return _onLMBInteraction; } }

        private Interaction _onRMBInteraction;
        public Interaction OnRMBInteraction { get { return _onRMBInteraction; } }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);
        }

        private void Update()
        {
            _onDefaultInteraction = null;
            _onLMBInteraction = null;
            _onRMBInteraction = null;

            if (UIManager.Instance.IsPageOpended || Player.SleepingManager.IsSleeping)
            {
                return;
            }

            IInteractable interactable = GetNearestInteractableOnRay();

            if (interactable == null)
            {
                return;
            }

            List<Interaction> interactings = interactable.GetInteractions();
            _onDefaultInteraction = interactings.Find(
                    (interaction) =>
                    interaction.Method == InteractingMethod.DefaultInteract
                    && interaction.GetIsAvailable(PlayerController.Instance));

            _onLMBInteraction = interactings.Find(
                    (interaction) =>
                    interaction.Method == InteractingMethod.LMBInteract
                    && interaction.GetIsAvailable(PlayerController.Instance));

            _onRMBInteraction = interactings.Find(
                    (interaction) =>
                    interaction.Method == InteractingMethod.RMBInteract
                    && interaction.GetIsAvailable(PlayerController.Instance));

            if (InputManager.Instance.IsInteract)
            {
                if (_onDefaultInteraction == null)
                {
                    return;
                }

                _onDefaultInteraction.OnInteract.Invoke(_player);
            }
            else if (InputManager.Instance.IsLMB)
            {
                if (_onLMBInteraction == null)
                {
                    return;
                }

                _onLMBInteraction.OnInteract.Invoke(_player);
            }
            else if (InputManager.Instance.IsRMB)
            {
                if (_onRMBInteraction == null)
                {
                    return;
                }

                _onRMBInteraction.OnInteract.Invoke(_player);
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

            if (distanceToNearest > _maxInteractionDistance)
            {
                return null;
            }

            return nearest;
        }
    }
}
