using System.Collections.Generic;
using System;

namespace Effects
{
    public static class EffectsManager
    {
        private static readonly Dictionary<string, Func<int, float, TemporaryEffect>> _temporaryEffect = new() {
            {EffectIds.Speed, (int level, float duration) => new TemporaryEffect(
                    EffectIds.Speed,
                    "Скорость",
                    level,
                    5,
                    new EffectBuffs(0.2f, 0),
                    duration)},

            {EffectIds.JumpBoost, (int level, float duration) => new TemporaryEffect(
                    EffectIds.Speed,
                    "Прыгучесть",
                    level,
                    5,
                    new EffectBuffs(0, 0.2f),
                    duration)}
        };

        private static readonly Dictionary<string, Func<int, PermanentEffect>> _permanentEffect =
            new() { };

        public static TemporaryEffect GetTemporary(string id, int level, float duration)
        {
            return _temporaryEffect[id].Invoke(level, duration);
        }

        public static PermanentEffect GetPermanent(string id, int level)
        {
            return _permanentEffect[id].Invoke(level);
        }
    }
}
