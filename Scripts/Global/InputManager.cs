using UnityEngine;
using Global.GlobalEvents;

namespace Global.InputManager
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;

        public static InputManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogError("InputManager instance isn't setted");
                }

                return _instance;
            }
        }

        private Vector2 _axes = new(0, 0);
        private Vector2 _cameraMove = new(0, 0);
        private bool _isInventory = false;
        private bool _isJump = false;
        private bool _isInteract = false;
        private bool _isLMB = false;
        private bool _isRMB = false;
        private bool _isSprinting = false;
        private bool _isMenu = false;

        public Vector2 Axes
        {
            get
            {
                return _axes;
            }
        }

        public Vector2 CameraMove
        {
            get
            {
                return new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            }
        }

        public bool IsInventory
        {
            get
            {
                return _isInventory;
            }
        }

        public bool IsJump

        {
            get
            {
                return _isJump;
            }
        }

        public bool IsInteract
        {
            get
            {
                return _isInteract;
            }
        }

        public bool IsLMB
        {
            get
            {
                return _isLMB;
            }
        }

        public bool IsRMB
        {
            get
            {
                return _isRMB;
            }
        }

        public bool IsSprinting
        {
            get
            {
                return _isSprinting;
            }
        }

        public bool IsMenu
        {
            get
            {
                return _isMenu;
            }
        }

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Update()
        {
            Vector2 axes = new();

            if (Input.GetKey(Preferences.PreferencesManager.Instance.MoveForwardButton))
            {
                axes.y += 1;
            }

            if (Input.GetKey(Preferences.PreferencesManager.Instance.MoveBackwardButton))
            {
                axes.y -= 1;
            }

            if (Input.GetKey(Preferences.PreferencesManager.Instance.MoveRightButton))
            {
                axes.x += 1;
            }

            if (Input.GetKey(Preferences.PreferencesManager.Instance.MoveLeftButton))
            {
                axes.x -= 1;
            }

            _axes = axes.normalized;

            if (Input.GetKeyDown(Preferences.PreferencesManager.Instance.InventoryButton))
            {
                GlobalEventsManager.Instance.Input.OnInventory.Invoke();
            }

            _isJump = Input.GetKey(Preferences.PreferencesManager.Instance.JumpButton);

            _isInteract = false;

            if (Input.GetKeyDown(Preferences.PreferencesManager.Instance.InteractButton))
            {
                GlobalEventsManager.Instance.Input.OnInteract.Invoke();
                _isInteract = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GlobalEventsManager.Instance.Input.OnLMB.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                GlobalEventsManager.Instance.Input.OnRMB.Invoke();
            }

            _isLMB = Input.GetMouseButton(0);
            _isRMB = Input.GetMouseButton(1);

            _isSprinting = Input.GetKey(Preferences.PreferencesManager.Instance.SprintButton);

            _isMenu = Input.GetKey(Preferences.PreferencesManager.Instance.MenuButton);

            if (Input.GetKeyDown(Preferences.PreferencesManager.Instance.MenuButton))
            {
                GlobalEventsManager.Instance.Input.OnMenu.Invoke();
            }

            if (Input.GetKeyDown(Preferences.PreferencesManager.Instance.QuestsButton))
            {
                GlobalEventsManager.Instance.Input.OnQuests.Invoke();
            }
        }
    }
}
