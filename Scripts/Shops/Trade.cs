using System.Collections.Generic;
using System.Collections.ObjectModel;
using Items.Storages;
using Items;
using UnityEngine;

namespace Shops
{
    public class Trade
    {
        private string _id;
        public string Id { get { return _id; } }

        private Dictionary<string, int> _input;
        public ReadOnlyDictionary<string, int> Input { get { return new(_input); } }

        private Dictionary<string, int> _output;
        public ReadOnlyDictionary<string, int> Output { get { return new(_output); } }

        public void MakeDeal(Inventory buyer, Inventory seller)
        {
            if (!IsAvailable(buyer, seller))
            {
                return;
            }

            foreach (KeyValuePair<string, int> item in _input)
            {
                seller.AddItem(buyer.GetItem(item.Key, item.Value));
            }

            foreach (KeyValuePair<string, int> item in _output)
            {
                buyer.AddItem(seller.GetItem(item.Key, item.Value));
            }
        }

        public bool IsAvailable(Inventory buyer, Inventory seller)
        {
            return buyer.IsEnoughResources(_input) && seller.IsEnoughResources(_output);
        }


        public Trade(string id, Dictionary<string, int> input, Dictionary<string, int> output)
        {
            _id = id;
            _input = input;
            _output = output;
        }

        public Trade(Serialization.Trade trade)
        {
            _id = trade.Id;
            _input = trade.Input;
            _output = trade.Output;
        }
    }
}
