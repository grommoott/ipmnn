using UnityEngine;
using Shops;
using Player;
using System.Linq;
using UnityEngine.UI;

namespace UI.Pages.Shop
{
    public class ShopPage : Page
    {
        [SerializeField] private GameObject _tradeElement;
        [SerializeField] private Transform _tradesListTransform;
        [SerializeField] private Button _dealButton;

        private Shops.Shop _shop = null;
        private Trade _selectedTrade = null;

        private void Start()
        {
            if (_shop == null)
            {
                Debug.LogError("Shop in shop page isn't setted");
                return;
            }

            _dealButton.onClick.AddListener(() => MakeDeal());

            UpdatePage();
        }

        private void UpdatePage()
        {
            for (int i = 0; i < _tradesListTransform.childCount; i++)
            {
                TradeElement el = _tradesListTransform.GetChild(i).GetComponent<TradeElement>();
                el.gameObject.SetActive(false);
                el.Destroy();
            }

            foreach (Trade trade in _shop.Trades)
            {
                TradeElement element =
                    Instantiate(_tradeElement, _tradesListTransform)
                    .GetComponent<TradeElement>();
                element.SetTrade(trade);
                element.SetSelected(trade.Id == _selectedTrade?.Id);
                element.SetCallback(OnTradeSelect);
                element.SetAvailable(trade.IsAvailable(PlayerController.Instance.InventoryManager.Inventory, _shop.Inventory));
            }
        }

        private void OnTradeSelect(string id)
        {
            _selectedTrade = _shop.Trades.First(trade => trade.Id == id);
            UpdatePage();
        }

        public void MakeDeal()
        {
            if (_selectedTrade == null)
            {
                return;
            }

            _selectedTrade.MakeDeal(PlayerController.Instance.InventoryManager.Inventory, _shop.Inventory);
        }

        public void SetShop(Shops.Shop shop)
        {
            _shop = shop;
        }

        public static ShopPage SpawnPage(Shops.Shop shop)
        {
            GameObject pageGO = UIManager.Instance.InstantiatePage(UIManager.Instance.ShopPage);
            ShopPage page = pageGO.GetComponent<ShopPage>();
            page.SetShop(shop);

            return page;
        }
    }
}
