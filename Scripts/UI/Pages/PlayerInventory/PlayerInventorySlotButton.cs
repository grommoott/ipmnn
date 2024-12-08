using UnityEngine;
using Items;
using UnityEngine.UI;
using Items.Storages;
using Player;

namespace UI.Pages.PlayerInventory
{
    public class PlayerInventorySlotButton : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryPage _mainPage;
        [SerializeField] private PlayerInventoryItemPanel _itemPanel;
        [SerializeField] private Image _image;
        [SerializeField] private PlayerInventorySlot _slot;
        [SerializeField] private Sprite _defaultSprite;
        private Item _item;
        private void Start()
        {
            if (!PlayerController.Instance.InventoryManager.Inventory.PlayerSlots.ContainsKey(_slot))
            {
                return;
            }

            SetItem(PlayerController.Instance.InventoryManager.Inventory.PlayerSlots[_slot]);
        }
        public void SetItem(Item item)
        {
            if (item == null)
            {
                _item = null;
                _image.sprite = _defaultSprite;
                return;
            }

            _item = item;
            _image.sprite = Resources.Load<Sprite>("Images/Items/" + item.Id);
        }
        public void SelectItem()
        {
            _itemPanel.Item = _item;
            _itemPanel.ItemType = ItemType.Equipped;
            _mainPage.UnmarkButtons();
            gameObject.GetComponent<Image>().color = new(0.75f, 0.75f, 0.75f);
            _itemPanel.Refresh();
        }
    }
}
