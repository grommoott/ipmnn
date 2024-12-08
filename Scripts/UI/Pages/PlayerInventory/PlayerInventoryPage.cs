using UnityEngine;
using UnityEngine.UI;
using Player;
using System.Collections.ObjectModel;
using Items;

namespace UI.Pages.PlayerInventory
{
    public class PlayerInventoryPage : Page
    {
        public Button MaterialButton;
        public Button ToolButton;
        public Button EquipableButton;
        public Button ConsumableButton;
        [SerializeField] private GameObject _subButton;
        [SerializeField] private Transform _contentContainer;
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private PlayerInventoryItemPanel _itemPanel;
        public Item Item;

        public void OnPressMaterial()
        {
            _itemType = ItemType.Material;
            ButtonColorChange(MaterialButton);
            Refresh();
        }
        public void OnPressTool()
        {
            _itemType = ItemType.Tool;
            ButtonColorChange(ToolButton);
            Refresh();
        }
        public void OnPressEquipable()
        {
            _itemType = ItemType.Clothing;
            ButtonColorChange(EquipableButton);
            Refresh();
        }
        public void OnPressConsumable()
        {
            _itemType = ItemType.Consumable;
            ButtonColorChange(ConsumableButton);
            Refresh();
        }
        private void ButtonColorChange(Button button)
        {
            Color white = Color.white;
            Color gray = new(0.75f, 0.75f, 0.75f);
            MaterialButton.gameObject.transform.Find("Background").GetComponent<Image>().color = white;
            ToolButton.gameObject.transform.Find("Background").GetComponent<Image>().color = white;
            EquipableButton.gameObject.transform.Find("Background").GetComponent<Image>().color = white;
            ConsumableButton.gameObject.transform.Find("Background").GetComponent<Image>().color = white;
            button.gameObject.transform.Find("Background").GetComponent<Image>().color = gray;
        }
        public void Refresh()
        {
            while (_contentContainer.childCount > 0)
                DestroyImmediate(_contentContainer.GetChild(0).gameObject);
            ReadOnlyCollection<Item> ItemList = PlayerController.Instance.InventoryManager.ItemList;
            foreach (Item item in ItemList)
                switch (_itemType)
                {
                    case ItemType.Material:
                        if (item is Items.Interfaces.IMaterial)
                            CreateSubButton(item);
                        break;
                    case ItemType.Tool:
                        if (item is Items.Interfaces.ITool or Items.Interfaces.IInteractingTool or Items.Interfaces.IUsable)
                            CreateSubButton(item);
                        break;
                    case ItemType.Clothing:
                        if (item is Items.Interfaces.IClothing)
                            CreateSubButton(item);
                        break;
                    case ItemType.Consumable:
                        if (item is Items.Interfaces.IConsumable or Items.Interfaces.IEatable)
                            CreateSubButton(item);
                        break;
                }
            _itemPanel.Deselect();
        }
        private void CreateSubButton(Item item)
        {
            GameObject subButton = Instantiate(_subButton, _contentContainer);
            subButton.GetComponent<PlayerInventoryItemButton>().SetItem(item, gameObject.GetComponent<PlayerInventoryPage>(), _itemPanel);
        }

        public static PlayerInventoryPage SpawnPage()
        {
            GameObject pageGO = UIManager.Instance.InstantiatePage(UIManager.Instance.PlayerInventoryPage);
            PlayerInventoryPage page = pageGO.GetComponent<PlayerInventoryPage>();

            return page;
        }
        public void SendInfo()
        {
            _itemPanel.Item = Item;
            _itemPanel.ItemType = _itemType;
        }
        public void UnmarkButtons()
        {
            for(int i = 0; i < _contentContainer.childCount; i++)
                _contentContainer.GetChild(i).GetComponent<Image>().color = Color.white;
            for(int i = 0; i < _slotsContainer.childCount; i++)
                _slotsContainer.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        private void Start()
        {
            Refresh();
        }
    }
}
