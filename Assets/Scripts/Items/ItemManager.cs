using System.Collections.Generic;

namespace Items
{
    public static class ItemManager
    {
        private static Dictionary<string, Item> _itemList = new();

        public static Item GetById(string id, int count)
        {
            Item item = _itemList[id].Clone();
            item.AddCount(count);

            return item;
        }
    }
}
