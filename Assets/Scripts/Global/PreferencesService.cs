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

        public KeyCode MoveForwardButton = KeyCode.W;
        public KeyCode MoveBackwardButton = KeyCode.S;
        public KeyCode MoveRightButton = KeyCode.D;
        public KeyCode MoveLeftButton = KeyCode.A;
        public KeyCode JumpButton = KeyCode.Space;
        public KeyCode InventoryButton = KeyCode.E;
        public KeyCode InteractButton = KeyCode.F;
        public KeyCode SprintButton = KeyCode.LeftControl;
    }
}
