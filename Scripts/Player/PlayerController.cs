using UnityEngine;
using Global.GlobalEvents;
using Interacting;
using System.Collections;
using Saves;
using UI;

namespace Player
{
    public class PlayerController : MonoBehaviour, IGlobalEventListener, IInteractor
    {
        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.Log("Player instance isn't setted");
                }

                return _instance;
            }
        }

        private PlayerInventoryManager _inventoryManager;
        public PlayerInventoryManager InventoryManager { get { return _inventoryManager; } }

        private PlayerMovementManager _movementManager;
        public PlayerMovementManager MovementManager { get { return _movementManager; } }

        private PlayerAnimationManager _animationManager;
        public PlayerAnimationManager AnimationManager { get { return _animationManager; } }

        private PlayerInteractingManager _interactingManager;
        public PlayerInteractingManager InteractingManager { get { return _interactingManager; } }

        private PlayerEffectsManager _effectsManager;
        public PlayerEffectsManager EffectsManager { get { return _effectsManager; } }

        private PlayerQuestManager _questManager;
        public PlayerQuestManager QuestManager { get { return _questManager; } }

        private PlayerStateManager _stateManager;
        public PlayerStateManager StateManager { get { return _stateManager; } }

        private PlayerSleepingManager _sleepingManager;
        public PlayerSleepingManager SleepingManager { get { return _sleepingManager; } }

        [SerializeField] private Camera _camera;
        public Camera Camera { get { return _camera; } }

        private void Awake()
        {
            _instance = this;

            _inventoryManager = GetComponent<PlayerInventoryManager>();
            _movementManager = GetComponent<PlayerMovementManager>();
            _animationManager = GetComponent<PlayerAnimationManager>();
            _interactingManager = GetComponent<PlayerInteractingManager>();
            _effectsManager = GetComponent<PlayerEffectsManager>();
            _questManager = GetComponent<PlayerQuestManager>();
            _stateManager = GetComponent<PlayerStateManager>();
            _sleepingManager = GetComponent<PlayerSleepingManager>();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GlobalEventsManager.Instance.Input.OnMenu.Subscribe
                (
                    OnMenu,
                    this
                );

            GlobalEventsManager.Instance.Input.OnInventory.Subscribe
                (
                    ToggleInventory,
                    this
                );

            GlobalEventsManager.Instance.Input.OnQuests.Subscribe
                (
                    ToggleQuests,
                    this
                );
        }

        private void Update()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Invoke();
        }

        public void OnDestroy()
        {
            GlobalEventsManager.Instance.Input.OnMenu.Unsubscribe(OnMenu, this);
            GlobalEventsManager.Instance.Input.OnInventory.Unsubscribe(ToggleInventory, this);
            GlobalEventsManager.Instance.Input.OnQuests.Unsubscribe(ToggleQuests, this);
        }

        private void ToggleInventory()
        {
            UIManager.Instance.OpenPage(UI.Pages.PlayerInventory.PlayerInventoryPage.SpawnPage());
        }

        private void ToggleQuests()
        {
            UIManager.Instance.OpenPage(UI.Pages.Quests.QuestsPage.SpawnPage());
        }

        private void OnMenu()
        {
            if (UIManager.Instance.IsPageOpended)
            {
                UIManager.Instance.ClosePage();
            }
            else
            {
                UIManager.Instance.OpenPage(UI.Pages.Menu.MenuPage.SpawnPage());
            }
        }

        public InteractorType GetInteractorType() => InteractorType.Player;
    }
}
