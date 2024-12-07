using UnityEngine;

namespace Effects
{
    public class TemporaryEffect : Effect
    {
        private EffectBuffs _buffsBase;
        private float _duration;
        public float Duration
        {
            get
            {
                return _duration;
            }
            private set
            {
                _duration = Mathf.Max(value, 0);
            }
        }

        public Effect AddEffect(TemporaryEffect other)
        {
            if (other.Id != Id)
            {
                return other;
            }

            Level = Mathf.Max(other.Level, Level);
            Duration = Mathf.Max((other as TemporaryEffect).Duration, Duration);

            return null;
        }

        public override EffectBuffs GetBuffs()
        {
            return _buffsBase.Multiply(Level);
        }

        public bool Update(float deltaTime)
        {
            Duration -= deltaTime;

            return Duration <= 0;
        }

        public TemporaryEffect(string id, string name, int level, int maxLevel, EffectBuffs buffs, float duration) : base(id, name, level, maxLevel)
        {
            _buffsBase = buffs;
            _duration = duration;
        }
    }
}

