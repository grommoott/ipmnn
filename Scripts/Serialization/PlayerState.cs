using System;
using Helpers;

namespace Serialization
{
    [Serializable]
    public class PlayerState
    {
        public float Stamine;
        public float Saturation;
        public Reputation Reputation;

        [Newtonsoft.Json.JsonConstructor]
        public PlayerState(float stamine, float saturation, Reputation reputation)
        {
            Stamine = stamine;
            Saturation = saturation;
            Reputation = reputation;
        }
    }
}
