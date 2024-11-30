using UnityEngine;
using Items.Storages;

namespace Player
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        private Player _player;
        public Player Player
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
            _player = GetComponent<Player>();
        }
    }
}
