using Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Items;
using System;
using Player;

namespace Quests
{
    public class QuestRewards
    {
        private List<Item> _items;
        public ReadOnlyCollection<Item> Items
        {
            get
            {
                return _items.AsReadOnly();
            }
        }

        private Reputation _reputation;
        public Reputation Reputation
        {
            get
            {
                return _reputation;
            }
        }

        private Action _onComplete;

        public void GiveRewards()
        {
            foreach (Item item in _items)
            {
                PlayerController.Instance.InventoryManager.AddItem(item);
            }

            if (_onComplete != null)
            {
                _onComplete.Invoke();
            }
        }

        public QuestRewards(List<Item> items, Reputation reputation, Action onComplete)
        {
            _items = items;
            _reputation = reputation;
            _onComplete = onComplete;
        }
    }
}
