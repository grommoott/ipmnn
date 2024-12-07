using Items.Storages;

namespace Items.Interfaces
{
    public interface IEquipable
    {
        public void Equip();
        public void Unequip();
        public PlayerInventorySlot GetSlot();
    }
}
