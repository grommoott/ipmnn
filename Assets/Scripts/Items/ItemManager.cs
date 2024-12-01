using System.Collections.Generic;

namespace Items
{
    public static class ItemManager
    {
        private static Dictionary<string, Item> _itemList = new()
        {
            {ItemIds.EnergyHoney, new Item(0, ItemIds.EnergyHoney, "Энергомёд", "Не тормози")}
        };

        public static Item GetById(string id, int count)
        {
            Item item = _itemList[id].Clone();
            item.AddCount(count);

            return item;
        }
    }
}
