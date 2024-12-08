using Items;
using System.Collections.Generic;
using Items.Storages;

namespace Shops
{
    public class ShopInventory : Inventory
    {
        public ShopInventory(List<Item> items) : base(items) { }
    }
}
