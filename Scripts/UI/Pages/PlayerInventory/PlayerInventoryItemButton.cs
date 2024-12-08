using UnityEngine;
using TMPro;
using Items;
using UnityEngine.UI;

namespace UI.Pages.PlayerInventory
{
    public class PlayerInventoryItemButton : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryPage _mainPage;
        [SerializeField] private PlayerInventoryItemPanel _itemPanel;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        private Item _item;
        public void SetItem(Item item, PlayerInventoryPage mainPage, PlayerInventoryItemPanel itemPanel)
        {
            _mainPage = mainPage;
            _itemPanel = itemPanel;
            _item = item;
            _text.text = item.Name;
            _image.sprite = Resources.Load<Sprite>("Images/Items/" + item.Id);
        }
        public void SelectItem()
        {
            _mainPage.Item = _item;
            _mainPage.SendInfo();
            _mainPage.UnmarkButtons();
            gameObject.GetComponent<Image>().color = new(0.75f,0.75f,0.75f);
            _itemPanel.Refresh();
        }
    }
}
