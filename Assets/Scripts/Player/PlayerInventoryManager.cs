using UnityEngine;
using Items.Storages;

namespace Player
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        private PlayerController _player;
        public PlayerController Player
        {
            get
            {
                return _player;
            }
        }

        private PlayerInventory _inventory;
        public PlayerInventory Inventory
        {
            get
            {
                return _inventory;
            }
        }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
            _inventory = new PlayerInventory();
        }
    }
}
