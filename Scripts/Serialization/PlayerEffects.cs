using System;
using System.Linq;
using System.Collections.Generic;

namespace Serialization
{
    [Serializable]
    public class PlayerEffects
    {
        public List<TemporaryEffect> TemporaryEffects = new();
        public List<PermanentEffect> PermanentEffects = new();

        [Newtonsoft.Json.JsonConstructor]
        public PlayerEffects(List<TemporaryEffect> temporaryEffects, List<PermanentEffect> permanentEffects)
        {
            TemporaryEffects = temporaryEffects;
            PermanentEffects = permanentEffects;
        }

        public PlayerEffects
            (
                Dictionary<string, Effects.TemporaryEffect> temporaryEffects,
                Dictionary<string, Effects.PermanentEffect> permamentEffects
            )
        {
            if (temporaryEffects != null)
            {
                TemporaryEffects = temporaryEffects.Values.Select((effect) => new TemporaryEffect(effect)).ToList();
            }

            if (permamentEffects != null)
            {
                PermanentEffects = permamentEffects.Values.Select((effect) => new PermanentEffect(effect)).ToList();
            }
        }

        public PlayerEffects() : this(new List<TemporaryEffect>(), new()) { }
    }
}
