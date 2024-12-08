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
            {ItemIds.EnergyHoney, (count) => new Material(count, ItemIds.EnergyHoney, "Банка энергомёдa", "Не тормози!")},
            {ItemIds.Wires, (count) => new Material(count, ItemIds.Wires, "Провода", "Ценный ресурс среди конструкторов.")},
            {ItemIds.Mushroom, (count) => new Material(count, ItemIds.Mushroom, "Гриб", "Это же мой гриб! Я его ем!")},
            {ItemIds.Bee, (count) => new Material(count, ItemIds.Bee, "Пчела", "Ну и юродивый...")},
            {ItemIds.Net, (count) => new Net(count)},
            {ItemIds.Honey, (count) => new Material(count, ItemIds.Honey, "Банка мёда", "Съедобная(не банка), используется в качестве торговой валюты. Ходят слухи что медведей опьеняет не только её вкус...")}
        };

        public static Item GetById(string id, int count)
        {
            if (!_itemList.ContainsKey(id))
            {
                UnityEngine.Debug.LogError("ItemManager have not item with id " + id);
            }
            Item item = _itemList[id].Invoke(count);

            return item;
        }
    }
}
