using UnityEngine;

namespace Helpers
{
    public class Reputation
    {
        public static readonly float MinReputation = 0;
        public static readonly float MaxReputation = 500;

        private float _programmers;
        public float Programmers
        {
            get { return _programmers; }
            set
            {
                _programmers = Mathf.Clamp(value, MinReputation, MaxReputation);
            }
        }

        private float _constructors;
        public float Constructors
        {
            get { return _constructors; }
            set
            {
                _constructors = Mathf.Clamp(value, MinReputation, MaxReputation);
            }
        }

        private float _himbio;
        public float Himbio
        {
            get { return _himbio; }
            set
            {
                _himbio = Mathf.Clamp(value, MinReputation, MaxReputation);
            }
        }

        private float _beekeepers;
        public float Beekeepers
        {
            get { return _beekeepers; }
            set
            {
                _beekeepers = Mathf.Clamp(value, MinReputation, MaxReputation);
            }
        }

        private float _spaceshifters;
        public float Spaceshifters
        {
            get { return _spaceshifters; }
            set
            {
                _spaceshifters = Mathf.Clamp(value, MinReputation, MaxReputation);
            }
        }

        private float _creators;
        public float Creators
        {
            get { return _creators; }
            set
            {
                _creators = Mathf.Clamp(value, MinReputation, MaxReputation);
            }
        }

        [Newtonsoft.Json.JsonConstructor]
        public Reputation(float programmers, float constructors, float himbio, float beekeepers, float spaceshifters, float creators)
        {
            Programmers = programmers;
            Constructors = constructors;
            Himbio = himbio;
            Beekeepers = beekeepers;
            Spaceshifters = spaceshifters;
            Creators = creators;
        }

        public Reputation()
            : this
                (
                100,
                100,
                100,
                100,
                100,
                100
                )
        { }
    }
}
