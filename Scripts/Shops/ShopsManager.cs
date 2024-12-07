using UnityEngine;
using Saves;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Shops
{
    public class ShopsManager : MonoBehaviour, ISaveable
    {
        private static ShopsManager _instance;
        public static ShopsManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("ShopsManager instance isn't setted");
                }

                return _instance;
            }
        }

        private Dictionary<string, Shop> _shops = new();

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }

            _instance = this;
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);
        }

        public Shop GetById(string id)
        {
            return _shops[id];
        }

        public void AddShop(Shop shop)
        {
            _shops.Add(shop.Id, shop);
        }

        public void RemoveShop(string id)
        {
            _shops.Remove(id);
        }

        private Dictionary<string, Shop> GetDefaultShops()
        {
            List<Trade> urmartalykShopTrades = new()
            {
                new Trade("net", new() { { Items.ItemIds.EnergyHoney, 10 } }, new() { { Items.ItemIds.Net, 1 } })
            };
            ShopInventory urmartalykShopInventory = new ShopInventory(new() { Items.ItemManager.GetById(Items.ItemIds.Net, 3) });
            Shop urmartalykShop = new Shop(ShopIds.Umartalyk, urmartalykShopInventory, urmartalykShopTrades);

            return new()
            {
                {urmartalykShop.Id, urmartalykShop}
            };
        }

        public void Load(JObject data)
        {
            if (data == null)
            {
                _shops = GetDefaultShops();
                return;
            }

            List<Serialization.Shop> shops = data.GetValue("shops").ToObject<List<Serialization.Shop>>();

            if (shops == null)
            {
                _shops = GetDefaultShops();
                return;
            }

            foreach (Serialization.Shop shop in shops)
            {
                _shops.Add(shop.Id, new Shop(shop));
            }
        }

        public object Save()
        {
            object result = _shops.Select(pair => new Serialization.Shop(pair.Value)).ToList();

            return new { shops = result };
        }

        public string GetSavingPath() => "shops";
    }
}
