using System;
using System.Collections.Generic;

namespace Serialization
{
    [Serializable]
    public class Trade
    {
        public string Id;
        public Dictionary<string, int> Input;
        public Dictionary<string, int> Output;

        [Newtonsoft.Json.JsonConstructor]
        public Trade(string id, Dictionary<string, int> input, Dictionary<string, int> output)
        {
            Id = id;
            Input = input;
            Output = output;
        }

        public Trade(Shops.Trade trade)
        {
            Id = trade.Id;
            Input = new(trade.Input);
            Output = new(trade.Output);
        }
    }
}
