using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Items.Interfaces;

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

        public void Equip(string id)
        {
            Item item = Items.First(item => item.Id == id);

            if (!(item is IEquipable))
            {
                return;
            }

            PlayerInventorySlot slot = (item as IEquipable).GetSlot();

            Item inInventory = GetItem(id);
            Item inSlot = GetItemInSlot(slot);
            AddItemInSlot(slot, inInventory);
            AddItem(inSlot);
        }

        public void Equip(string id, int count)
        {
            Item item = Items.First(item => item.Id == id);

            if (!(item is IEquipable))
            {
                return;
            }

            PlayerInventorySlot slot = (item as IEquipable).GetSlot();

            Item inInventory = GetItem(id, count);
            Item inSlot = GetItemInSlot(slot);
            AddItemInSlot(slot, inInventory);
            AddItem(inSlot);
        }

        public void Unqeuip(PlayerInventorySlot slot)
        {
            Item inSlot = GetItemInSlot(slot);
            AddItem(inSlot);
        }

        public Item GetItemInSlot(PlayerInventorySlot slot, int count)
        {
            if (_playerSlots.GetValueOrDefault(slot, null) == null)
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

        public Item GetItemInSlot(PlayerInventorySlot slot)
        {
            if (_playerSlots.GetValueOrDefault(slot, null) == null)
            {
                return null;
            }

            Item item = _playerSlots[slot].GetAll();

            if (_playerSlots[slot].Count == 0)
            {
                _playerSlots[slot] = null;
            }

            return item;
        }

        public Item AddItemInSlot(PlayerInventorySlot slot, Item item)
        {
            if (_playerSlots.GetValueOrDefault(slot, null) != null)
            {
                return _playerSlots[slot].AddItem(item);
            }
            else
            {
                _playerSlots[slot] = item;
                return null;
            }
        }

        public PlayerInventory()
            : this
              (
               new(),
               null,
               null,
               null,
               null,
               null
               )
        { }

        public PlayerInventory(List<Item> items, Item itemInHand, Item itemOnHead, Item itemOnBody, Item itemOnLegs, Item itemOnBoots)
            : base(items)
        {
            if (itemInHand != null)
            {
                _playerSlots[PlayerInventorySlot.Hand] = itemInHand;
            }

            if (itemOnHead != null)
            {
                _playerSlots[PlayerInventorySlot.Head] = itemOnHead;
            }

            if (itemOnBody != null)
            {
                _playerSlots[PlayerInventorySlot.Body] = itemOnBody;
            }

            if (itemOnLegs != null)
            {
                _playerSlots[PlayerInventorySlot.Legs] = itemOnLegs;
            }

            if (itemOnBoots != null)
            {
                _playerSlots[PlayerInventorySlot.Boots] = itemOnBoots;
            }
        }

        public Serialization.PlayerInventory Serialize()
        {
            Item[] items = new Item[Items.Count];
            Items.CopyTo(items, 0);

            return new Serialization.PlayerInventory
                (
                    items,
                    PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Hand, null),
                    PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Head, null),
                    PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Body, null),
                    PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Legs, null),
                    PlayerSlots.GetValueOrDefault(PlayerInventorySlot.Boots, null)
                );
        }

        public static PlayerInventory Parse(Serialization.PlayerInventory data)
        {
            List<Item> items = data.Items
                .Select((item) => ItemManager.GetById(item.Id, item.Count))
                .ToList();

            return new PlayerInventory
                (
                    items,
                    data.ItemInHand?.ToItem(),
                    data.ItemOnHead?.ToItem(),
                    data.ItemOnBody?.ToItem(),
                    data.ItemOnLegs?.ToItem(),
                    data.ItemOnBoots?.ToItem()
                );
        }
    }
}
