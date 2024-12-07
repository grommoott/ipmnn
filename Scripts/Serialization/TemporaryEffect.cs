using System;

namespace Serialization
{
    [Serializable]
    public class TemporaryEffect
    {
        public string Id;
        public int Level;
        public float Duration;

        [Newtonsoft.Json.JsonConstructor]
        public TemporaryEffect(string id, int level, float duration)
        {
            Id = id;
            Level = level;
            Duration = duration;
        }

        public TemporaryEffect(Effects.TemporaryEffect effect)
            : this(effect.Id, effect.Level, effect.Duration) { }
    }
}
