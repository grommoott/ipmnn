using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Items;

namespace UI.Pages.Shop
{
    public class TradeItem : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _countText;

        public void SetItem(Item item)
        {
            _image.sprite = Resources.Load<Sprite>($"Images/Items/{item.Id}");
            _countText.text = item.Count.ToString();
        }
    }
}
