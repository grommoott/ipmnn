using Items.Storages;
using System.Collections.Generic;

namespace Items.Interfaces
{
    public interface IEquipable
    {
        public void Equip(PlayerInventorySlot slot);
        public void Unequip(PlayerInventorySlot slot);
        public PlayerInventorySlot GetSlotAvailable();
    }
}
