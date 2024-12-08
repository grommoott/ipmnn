using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System.Linq;

namespace Items.Storages
{
    public class Inventory
    {
        private List<Item> _items = new();
        public ReadOnlyCollection<Item> Items
        {
            get
            {
                return _items.AsReadOnly();
            }
        }

        public bool IsEnoughResources(Dictionary<string, int> resouces)
        {
            foreach (KeyValuePair<string, int> item in resouces)
            {
                Item tmp = GetItem(item.Key, item.Value);

                if (tmp == null || tmp.Count == 0)
                {
                    return false;
                }

                if (tmp.Count < item.Value)
                {
                    AddItem(tmp);
                    return false;
                }

                AddItem(tmp);
            }

            return true;
        }

        public bool IsEnoughResources(List<Item> items)
        {
            List<int> counts = items.Select(item => item.Count).ToList();
            List<string> ids = items.Select(item => item.Id).ToList();

            return IsEnoughResources(
                    ids.Zip(counts,
                        (key, value) => new { key, value }).ToDictionary(pair => pair.key, pair => pair.value));
        }

        public Item AddItem(Item item)
        {
            if (item == null || item.Count == 0)
            {
                return null;
            }

            int count = _items.Count;

            for (int i = 0; i < count && item.Count != 0; i++)
            {
                item = _items[i].AddItem(item);

                if (item == null)
                {
                    break;
                }
            }

            if (item != null)
            {
                _items.Add(item);
            }

            return null;
        }

        public Item GetItem(string id, int count)
        {
            Item item = ItemManager.GetById(id, 0);

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].IsStackable(item))
                {
                    item.AddItem(_items[i].GetCount(count - item.Count));



                    if (_items[i].Count == 0)
                    {
                        _items.RemoveAt(i);
                        i--;
                    }

                    if (item.Count == count)
                    {
                        break;
                    }
                }
            }

            return item;
        }

        public Item GetItem(string id)
        {
            Item item = ItemManager.GetById(id, 0);

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].IsStackable(item))
                {
                    item.AddItem(_items[i].GetCount(_items[i].Count));
                    _items.RemoveAt(i);
                    break;
                }
            }

            return item;
        }

        public Inventory(List<Item> items)
        {
            _items = items;
        }
    }
}
