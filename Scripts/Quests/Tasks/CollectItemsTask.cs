using Global.GlobalEvents;
using System.Collections.Generic;
using Items;
using System.Text;
using System.Linq;

namespace Quests.Tasks
{
    public class CollectItemsTask : QuestTask
    {
        private List<Item> _items;

        override public string GetProgress()
        {
            StringBuilder builder = new StringBuilder();

            foreach (Item item in _items)
            {
                Item inInventory =
                    Player.PlayerController.Instance.InventoryManager.ItemList.First(item => item.Id == item.Id);

                builder.AppendJoin("\n", $"Собрано ${inInventory.Count} \"${item.Name}\" из ${item}");
            }

            return builder.ToString();
        }

        override protected void OnInitialize()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Subscribe(OnUpdate, this);
        }

        private void OnUpdate()
        {
            if (Player.PlayerController.Instance.InventoryManager.Inventory.IsEnoughResources(_items))
            {
                foreach (Item item in _items)
                {
                    Player.PlayerController.Instance.InventoryManager.Inventory.GetItem(item.Id, item.Count);
                }

                OnComplete();
            }
        }

        override public void OnDestroy()
        {
            GlobalEventsManager.Instance.Game.OnUpdate.Unsubscribe(OnUpdate, this);
        }

        public CollectItemsTask(string description, List<Item> items) : base(description)
        {
            _items = items;
        }
    }
}
