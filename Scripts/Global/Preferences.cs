using UnityEngine;
using Newtonsoft.Json.Linq;
using Saves;

namespace Global.Preferences
{
    public class PreferencesManager : MonoBehaviour, ISaveable
    {
        private static PreferencesManager _instance;

        public static PreferencesManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("PreferencesManager instance isn't setted");
                }

                return _instance;
            }
        }

        public KeyCode MoveForwardButton { get; private set; } = KeyCode.W;
        public KeyCode MoveBackwardButton { get; private set; } = KeyCode.S;
        public KeyCode MoveRightButton { get; private set; } = KeyCode.D;
        public KeyCode MoveLeftButton { get; private set; } = KeyCode.A;
        public KeyCode JumpButton { get; private set; } = KeyCode.Space;
        public KeyCode InventoryButton { get; private set; } = KeyCode.E;
        public KeyCode InteractButton { get; private set; } = KeyCode.F;
        public KeyCode SprintButton { get; private set; } = KeyCode.LeftControl;
        public KeyCode MenuButton { get; private set; } = KeyCode.Escape;
        public KeyCode QuestsButton { get; private set; } = KeyCode.Q;

        private float _sensitivity = 300f;
        public float SensitivityMultiplier = 1;
        public float Sensitivity { get { return _sensitivity * SensitivityMultiplier; } }

        private float _volume = 1;
        public float VolumeMultiplier = 0.3f;
        public float Volume { get { return _volume * VolumeMultiplier; } }

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public void Load(JObject data)
        {
            if (data == null)
            {
                return;
            }

            Serialization.Preferences preferences = data.ToObject<Serialization.Preferences>();

            SensitivityMultiplier = preferences.SensitivityMultiplier;
            VolumeMultiplier = preferences.VolumeMultiplier;
        }

        public object Save()
        {
            return new Serialization.Preferences(this);
        }

        public string GetSavingPath() => "preferences";
    }
}
