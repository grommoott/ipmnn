using TMPro;
using UnityEngine;
using Items;
using Player;

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
        [SerializeField] private GameObject _useButton;
        [SerializeField] private GameObject _deselectButton;
        public Item Item;
        public ItemType ItemType;
        public void Refresh()
        {
            if(Item != null)
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
            switch(ItemType)
            {
                case ItemType.Tool or ItemType.Clothing:
                    _useButton.SetActive(false);
                    _equipButton.SetActive(true);
                    break;
                case ItemType.Consumable:
                    _equipButton.SetActive(false);
                    _useButton.SetActive(true);
                    break;
            }
        }
        public void Deselect()
        {
            _destroyButton.SetActive(false);
            _deselectButton.SetActive(false);
            _useButton.SetActive(false);
            _equipButton.SetActive(false);
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
    }
}
