using UnityEngine;

namespace Effects
{
    public class PermanentEffect : Effect
    {
        private EffectBuffs _buffsBase;

        public PermanentEffect AddEffect(PermanentEffect other)
        {
            if (other.Id != Id)
            {
                return other;
            }

            Level = Mathf.Max(other.Level, Level);

            return null;
        }

        public override EffectBuffs GetBuffs()
        {
            return _buffsBase.Multiply(Level);
        }

        public PermanentEffect(string id, string name, int level, int maxLevel, EffectBuffs buffs)
            : base(id, name, level, maxLevel)
        {
            _buffsBase = buffs;
        }
    }
}
