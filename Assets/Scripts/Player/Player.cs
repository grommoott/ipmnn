using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        private static Player _instance;
        public static Player Instance
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

        private void Awake()
        {
            _inventoryManager = GetComponent<PlayerInventoryManager>();
            _movementManager = GetComponent<PlayerMovementManager>();
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
