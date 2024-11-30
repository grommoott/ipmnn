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
                return _cameraMove;
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

            if (Input.GetKey(PreferencesService.PreferencesService.Instance.MoveForwardButton))
            {
                axes.y += 1;
            }

            if (Input.GetKey(PreferencesService.PreferencesService.Instance.MoveBackwardButton))
            {
                axes.y -= 1;
            }

            if (Input.GetKey(PreferencesService.PreferencesService.Instance.MoveRightButton))
            {
                axes.x += 1;
            }

            if (Input.GetKey(PreferencesService.PreferencesService.Instance.MoveLeftButton))
            {
                axes.x -= 1;
            }

            _axes = axes;

            Vector2 cameraMove = Input.mousePositionDelta;
            cameraMove.y = -cameraMove.y;
            _cameraMove = cameraMove;

            if (Input.GetKeyDown(PreferencesService.PreferencesService.Instance.InventoryButton))
            {
                GlobalEventsManager.Instance.OnInventory.Invoke();
            }

            _isJump = Input.GetKey(PreferencesService.PreferencesService.Instance.JumpButton);

            if (Input.GetKeyDown(PreferencesService.PreferencesService.Instance.InteractButton))
            {
                GlobalEventsManager.Instance.OnInteract.Invoke();
            }

            if (Input.GetMouseButtonDown(0))
            {
                GlobalEventsManager.Instance.OnLMB.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                GlobalEventsManager.Instance.OnRMB.Invoke();
            }

            _isLMB = Input.GetMouseButton(0);
            _isRMB = Input.GetMouseButton(1);

            _isSprinting = Input.GetKey(PreferencesService.PreferencesService.Instance.SprintButton);
        }
    }
}
