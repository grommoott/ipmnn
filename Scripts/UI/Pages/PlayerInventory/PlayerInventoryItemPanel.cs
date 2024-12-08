using TMPro;
using UnityEngine;
using Items;
using Player;
using Items.Storages;
using Items.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UI.Pages.PlayerInventory
{
    public class PlayerInventoryItemPanel : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryPage _mainPage;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private GameObject _destroyButton;
        [SerializeField] private GameObject _equipButton;
        [SerializeField] private GameObject _unequipButton;
        [SerializeField] private GameObject _useButton;
        [SerializeField] private GameObject _deselectButton;
        [SerializeField] private List<PlayerInventorySlotButton> _slotButtons;
        [SerializeField] private List<PlayerInventorySlot> _slots = new() { PlayerInventorySlot.Hand };
        private Dictionary<PlayerInventorySlot, PlayerInventorySlotButton> _slotButtonsToSlots;
        public Item Item;
        public ItemType ItemType;
        private void Start()
        {
            _slotButtonsToSlots = _slots.Zip(_slotButtons, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
        }
        public void Refresh()
        {
            if (Item != null)
            {
                _name.text = Item.Name;
                _description.text = Item.Description;
                _count.text = "x" + Item.Count.ToString();
                _destroyButton.SetActive(true);
                _deselectButton.SetActive(true);
            }
            else
            {
                _name.text = "";
                _description.text = "";
                _count.text = "";
            }
            switch (ItemType)
            {
                case ItemType.Tool or ItemType.Clothing:
                    _equipButton.SetActive(true);
                    _unequipButton.SetActive(false);
                    _useButton.SetActive(false);
                    break;
                case ItemType.Consumable:
                    _equipButton.SetActive(false);
                    _unequipButton.SetActive(false);
                    _useButton.SetActive(true);
                    break;
                case ItemType.Equipped:
                    _equipButton.SetActive(false);
                    _unequipButton.SetActive(true);
                    _useButton.SetActive(false);
                    break;
            }
        }
        public void Deselect()
        {
            _destroyButton.SetActive(false);
            _deselectButton.SetActive(false);
            _useButton.SetActive(false);
            _equipButton.SetActive(false);
            _unequipButton.SetActive(false);
            Item = null;
            ItemType = ItemType.Material;
            Refresh();
            _mainPage.UnmarkButtons();
        }
        public void Destroy()
        {
            PlayerController.Instance.InventoryManager.GetItem(Item.Id, Item.Count);
            Deselect();
            _mainPage.Refresh();
        }
        public void Equip()
        {
            PlayerController.Instance.InventoryManager.Equip(Item.Id);
            SlotUpdate();
            Deselect();
            _mainPage.Refresh();
        }
        public void Unequip()
        {
            if (Item == null)
            {
                return;
            }

            SlotUpdate();
            PlayerController.Instance.InventoryManager.Unequip((Item as IEquipable).GetSlot());
            Deselect();
            _mainPage.Refresh();
        }

        private void SlotUpdate()
        {
            foreach (KeyValuePair<PlayerInventorySlot, PlayerInventorySlotButton> slotButton in _slotButtonsToSlots)
            {
                slotButton.Value.SetItem(PlayerController.Instance.InventoryManager.Inventory.PlayerSlots[slotButton.Key]); ;
            }
        }
    }
}
