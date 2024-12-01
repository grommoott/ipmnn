using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Items.Storages
{
    public class PlayerInventory : Inventory
    {
        private Dictionary<PlayerInventorySlot, Item> _playerSlots = new();

        public ReadOnlyDictionary<PlayerInventorySlot, Item> PlayerSlots
        {
            get
            {
                return new ReadOnlyDictionary<PlayerInventorySlot, Item>(_playerSlots);
            }
        }

        public Item GetItemInSlot(PlayerInventorySlot slot, int count)
        {
            if (_playerSlots[slot] == null)
            {
                return null;
            }

            Item item = _playerSlots[slot].GetCount(count);

            if (_playerSlots[slot].Count == 0)
            {
                _playerSlots[slot] = null;
            }

            return item;
        }

        public Item AddItemInSlot(PlayerInventorySlot slot, Item item)
        {
            if (_playerSlots[slot] != null)
            {
                return _playerSlots[slot].AddItem(item);
            }
            else
            {
                _playerSlots[slot] = item;
                return null;
            }
        }
    }
}
