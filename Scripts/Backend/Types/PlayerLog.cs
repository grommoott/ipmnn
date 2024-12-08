using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Backend.Types
{
    [Serializable]
    public class PlayerLog
    {
        [JsonProperty("comment")] public string Comment;
        [JsonProperty("player_name")] public string PlayerName;
        [JsonProperty("resources_changed")] public List<Serialization.Item> ResourcesChanged;
    }
}
