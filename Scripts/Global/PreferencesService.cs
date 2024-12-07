using UnityEngine;

namespace Global.PreferencesService
{
    public class PreferencesService
    {
        private static PreferencesService _instance;

        public static PreferencesService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PreferencesService();
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

        private float _sensitivity = 30f;
        public float SensitivityMultiplier = 1;
        public float Sensitivity { get { return _sensitivity * SensitivityMultiplier; } }

        private float _volume = 1;
        public float VolumeMultiplier = 1;
        public float Volume { get { return _volume * VolumeMultiplier; } }
    }
}
