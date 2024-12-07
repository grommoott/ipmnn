using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serialization
{
    [Serializable]
    public class PlayerInventory
    {
        public List<Item> Items;
        public Item ItemInHand = null;
        public Item ItemOnHead = null;
        public Item ItemOnBody = null;
        public Item ItemOnLegs = null;
        public Item ItemOnBoots = null;

        public PlayerInventory
            (
                Items.Item[] items,
                Items.Item itemInHand,
                Items.Item itemOnHead,
                Items.Item itemOnBody,
                Items.Item itemOnLegs,
                Items.Item itemOnBoots
            )
        {
            Items = new();

            foreach (Items.Item item in items)
            {
                Items.Add(new Item(item));
            }

            if (itemInHand != null)
            {
                ItemInHand = new Item(itemInHand);
            }

            if (itemOnHead != null)
            {
                ItemOnHead = new(itemOnHead);
            }

            if (itemOnBody != null)
            {
                ItemOnBody = new(itemOnBody);
            }

            if (itemOnLegs != null)
            {
                ItemOnLegs = new(itemOnLegs);
            }

            if (itemOnBoots != null)
            {
                ItemOnBoots = new(itemOnBoots);
            }
        }

        [Newtonsoft.Json.JsonConstructor]
        public PlayerInventory
            (
                Item[] items,
                Item itemInHand,
                Item itemOnHead,
                Item itemOnBody,
                Item itemOnLegs,
                Item itemOnBoots
                )
        {
            Items = new List<Item>(items);
            ItemInHand = itemInHand;
            ItemOnHead = itemOnHead;
            ItemOnBody = itemOnBody;
            ItemOnLegs = itemOnLegs;
            ItemOnBoots = itemOnBoots;
        }

        public PlayerInventory()
            : this
            (
                new Item[0],
                null,
                null,
                null,
                null,
                null
            )
        { }
    }
}
