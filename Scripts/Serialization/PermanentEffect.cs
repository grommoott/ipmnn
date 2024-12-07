using System;

namespace Serialization
{
    [Serializable]
    public class PermanentEffect
    {
        public string Id;
        public int Level;

        [Newtonsoft.Json.JsonConstructor]
        public PermanentEffect(string id, int level)
        {
            Id = id;
            Level = level;
        }

        public PermanentEffect(Effects.PermanentEffect effect) : this(effect.Id, effect.Level) { }
    }
}
