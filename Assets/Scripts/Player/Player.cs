using UnityEngine;
using Global.GlobalEvents;
using Items.Shell;
using Items;
using System;
using Interacting;

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
        public PlayerInventoryManager InventoryManager
        {
            get
            {
                return _inventoryManager;
            }
        }

        private PlayerMovementManager _movementManager;
        public PlayerMovementManager MovementManager
        {
            get
            {
                return _movementManager;
            }
        }

        private PlayerAnimationManager _animationManager;
        public PlayerAnimationManager AnimationManager
        {
            get
            {
                return _animationManager;
            }
        }

        private PlayerInteractingManager _interactingManager;
        public PlayerInteractingManager InteractingManager
        {
            get
            {
                return InteractingManager;
            }
        }

        [SerializeField] private Camera _camera;
        public Camera Camera
        {
            get
            {
                return _camera;
            }
        }

        private void Awake()
        {
            _inventoryManager = GetComponent<PlayerInventoryManager>();
            _movementManager = GetComponent<PlayerMovementManager>();
            _animationManager = GetComponent<PlayerAnimationManager>();
            _interactingManager = GetComponent<PlayerInteractingManager>();
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GlobalEventsManager.Instance.OnInventory.Subscribe(this);
        }

        public void OnDestroy()
        {
            GlobalEventsManager.Instance.OnInventory.Unsubscribe(this);
        }

        public Action GetCallback() => () =>
        {
            ItemShellManager.Instance.SpawnItem(ItemManager.GetById(ItemIds.EnergyHoney, 10), transform.position);
        };

        public InteractorType GetInteractorType() => InteractorType.Player;
    }
}
