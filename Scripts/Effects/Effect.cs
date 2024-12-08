using UnityEngine;

namespace Effects
{
    public abstract class Effect
    {
        private string _id;
        public string Id { get { return _id; } }

        private string _name;
        public string Name { get { return _name; } }

        int _level;
        public int Level
        {
            get { return _level; }
            protected set
            {
                _level = Mathf.Clamp(value, 1, _maxLevel);
            }
        }

        private int _maxLevel;
        public int MaxLevel
        {
            get { return _maxLevel; }

            private set
            {
                _maxLevel = Mathf.Max(value, 1);
            }
        }

        public abstract EffectBuffs GetBuffs();

        public Effect(string id, string name, int level, int maxLevel)
        {
            _id = id;
            _name = name;
            _level = level;
            MaxLevel = maxLevel;
        }
    }
}
