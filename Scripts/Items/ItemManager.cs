using System.Collections.Generic;
using Items.Classes.Materials;
using Items.Classes.InteractingTools;
using System;

namespace Items
{
    public static class ItemManager
    {
        private static Dictionary<string, Func<int, Item>> _itemList = new()
        {
            {ItemIds.EnergyHoney, (count) => new Material(count, ItemIds.EnergyHoney, "Энергомёд", "Не тормози!")},
            {ItemIds.Wires, (count) => new Material(count, ItemIds.Wires, "Провода", "Ценный ресурс среди конструкторов.")},
            {ItemIds.Mushroom, (count) => new Material(count, ItemIds.Mushroom, "Гриб", "Это же мой гриб! Я его ем!")},
            {ItemIds.Bee, (count) => new Material(count, ItemIds.Bee, "Пчела", "Ну и юродивый...")},
            {ItemIds.Net, (count) => new Net(count)},
        };

        public static Item GetById(string id, int count)
        {
            Item item = _itemList[id].Invoke(count);

            return item;
        }
    }
}
