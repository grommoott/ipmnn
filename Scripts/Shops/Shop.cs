using System.Collections.ObjectModel;
using System.Collections.Generic;
using Items.Storages;
using System.Linq;
using Items;

namespace Shops
{
    public class Shop
    {
        private string _id;
        public string Id { get { return _id; } }

        private ShopInventory _inventory;
        public ShopInventory Inventory { get { return _inventory; } }

        private List<Trade> _trades;
        public ReadOnlyCollection<Trade> Trades { get { return _trades.AsReadOnly(); } }

        public void AddTrade(Trade trade)
        {
            _trades.Add(trade);
        }

        public void RemoveTrade(string id)
        {
            _trades.RemoveAll((trade) => trade.Id == id);
        }

        public void MakeDeal(Trade trade, Inventory buyer)
        {
            trade.MakeDeal(buyer, Inventory);
        }

        public ReadOnlyCollection<Trade> GetAvailableTrades(Inventory buyer)
        {
            return _trades.Where((trade) => trade.IsAvailable(buyer, Inventory)).ToList().AsReadOnly();
        }

        public Shop(string id, ShopInventory inventory, List<Trade> trades)
        {
            _id = id;
            _inventory = inventory;
            _trades = trades;
        }

        public Shop(Serialization.Shop shop)
        {
            _id = shop.Id;
            _trades = shop.Trades.Select(trade => new Trade(trade)).ToList();
            _inventory = new ShopInventory(shop.Items.Select(item => ItemManager.GetById(item.Id, item.Count)).ToList());
        }
    }
}
