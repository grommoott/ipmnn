using System;
using System.Linq;
using System.Collections.Generic;

namespace Serialization
{
    [Serializable]
    public class Shop
    {
        public string Id;
        public List<Trade> Trades;
        public List<Item> Items;

        [Newtonsoft.Json.JsonConstructor]
        public Shop(string id, List<Trade> trades, List<Item> items)
        {
            Id = id;
            Trades = trades;
            Items = items;
        }

        public Shop(Shops.Shop shop)
        {
            Id = shop.Id;
            Trades = shop.Trades.Select((trade) => new Trade(trade)).ToList();
            Items = shop.Inventory.Items.Select(item => new Item(item)).ToList();
        }
    }
}
