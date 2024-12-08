using System;
using Newtonsoft.Json;

namespace Backend.Types
{
    [Serializable]
    public class Player
    {
        [JsonProperty("name")] public string Name;
        [JsonProperty("resources")] public Serialization.PlayerInventory Resources;
    }
}
