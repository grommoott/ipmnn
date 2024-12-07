using System;

namespace Serialization
{
    [Serializable]
    public class Item
    {
        public string Id;
        public int Count;

        [Newtonsoft.Json.JsonConstructor]
        public Item(string id, int count)
        {
            Id = id;
            Count = count;
        }

        public Item(Items.Item item)
        {
            Id = item.Id;
            Count = item.Count;
        }

        public Items.Item ToItem()
        {
            return Items.ItemManager.GetById(Id, Count);
        }
    }
}
