using UnityEngine;
using Global.GlobalEvents;
using Helpers;
using Saves;
using Newtonsoft.Json.Linq;

namespace Player
{
    public class PlayerStateManager : MonoBehaviour, ISaveable
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        private readonly MinMax _stamineMinMax = new MinMax(0, 100);
        public MinMax StamineMinMax { get { return _stamineMinMax; } }
        private float _stamine = 100;
        public float Stamine
        {
            get { return _stamine; }
            set
            {
                _stamine = Mathf.Clamp(value, _stamineMinMax.Min, _stamineMinMax.Max);
            }
        }

        private readonly MinMax _saturationMinMax = new MinMax(0, 100);
        public MinMax SaturationMinMax { get { return _stamineMinMax; } }
        private float _saturation = 100;
        public float Saturation
        {
            get { return _saturation; }
            set
            {
                _saturation = Mathf.Clamp(value, _saturationMinMax.Min, _saturationMinMax.Max);
            }
        }

        private Reputation _reputation = new Reputation();
        public Reputation Reputation
        {
            get { return _reputation; }
            set
            {
                _reputation = value;
                GlobalEventsManager.Instance.Player.OnReputationChange.Invoke();
            }
        }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        public void Load(JObject data)
        {
            if (data == null)
            {
                return;
            }

            Serialization.PlayerState state = data.ToObject<Serialization.PlayerState>();

            _stamine = state.Stamine;
            _saturation = state.Saturation;
            _reputation = state.Reputation;
        }

        public object Save()
        {
            return new Serialization.PlayerState(Stamine, Saturation, Reputation);
        }

        public string GetSavingPath() => "playerState";
    }
}
