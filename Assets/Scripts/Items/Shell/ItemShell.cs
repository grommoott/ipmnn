using UnityEngine;

namespace Items.Shell
{
    public class ItemShell : MonoBehaviour
    {
        private Item _item = null;

        public Item GetCount(int count)
        {
            Item item = _item.GetCount(count);

            if (_item.Count == 0)
            {
                Destroy(gameObject);
            }

            return item;
        }

        public Item AddCount(int count)
        {
            Item item = _item.AddCount(count);
            return item;
        }

        public void SetItem(Item item)
        {
            if (_item != null)
            {
                Debug.LogAssertion("Trying to set Item to ItemShell that already contains Item");
                return;
            }
            _item = item;


        }
    }
}
