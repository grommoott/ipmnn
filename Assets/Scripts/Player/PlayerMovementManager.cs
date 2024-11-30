using UnityEngine;
using Helpers;
using System.Collections.Generic;
using Global.InputManager;
using Global.PreferencesService;

namespace Player
{
    public class PlayerMovementManager : MonoBehaviour
    {
        private Player _player;
        public Player Player
        {
            get
            {
                return _player;
            }
        }

        private CharacterController _characterController;
        private LayerMask _surface;

        [SerializeField] private Transform _head;
        [SerializeField] private Trigger _floorTrigger;
        [SerializeField] private float _airFriction;
        [SerializeField] private float _airControllability;
        [SerializeField] private float _defaultSurfaceFriction;
        [SerializeField] private float _defaultSurfaceControllability;
        [SerializeField] private float _speed;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpVelocity;

        private Vector3 _velocity;
        private bool _isJumpAvailable;

        private List<float> _frictionRatios = new();
        private List<float> _controllabilityRatios = new();

        private void Awake()
        {
            _player = GetComponent<Player>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _floorTrigger.OnStay += OnFloorTriggerStay;
        }

        private void Update()
        {
            _head.Rotate(new Vector3(InputManager.Instance.CameraMove.y, 0, 0) * PreferencesService.Instance.Sensitivity);
            transform.Rotate(new Vector3(0, InputManager.Instance.CameraMove.x, 0) * PreferencesService.Instance.Sensitivity);

            /*if (_head.eulerAngles.x <= 0)*/
            /*{*/
            /*    _head.eulerAngles = new Vector3(0, _head.rotation.y, _head.rotation.z);*/
            /*}*/
            /*else if (_head.eulerAngles.x >= 180)*/
            /*{*/
            /*    _head.eulerAngles = new Vector3(180, _head.rotation.y, _head.rotation.z);*/
            /*}*/
        }

        private void FixedUpdate()
        {
            // сделать через Physics.OverlapSphere

            _frictionRatios.Add(_airFriction);
            _controllabilityRatios.Add(_airControllability);

            float _frictionRatio = 0;
            _frictionRatios.ForEach(ratio => _frictionRatio += ratio);
            _frictionRatio /= _frictionRatios.Count;

            float _controllabilityRatio = 0;
            _controllabilityRatios.ForEach(ratio => _controllabilityRatio += ratio);
            _controllabilityRatio /= _controllabilityRatios.Count;

            Vector2 axes = InputManager.Instance.Axes;
            Vector3 velocityDirection = transform.localToWorldMatrix.MultiplyVector(new Vector3(axes.x, 0, axes.y));

            if (_characterController.isGrounded)
            {
                _velocity.y = 0;

                if (InputManager.Instance.IsJump)
                {
                    _velocity.y = _jumpVelocity;
                }
            }
            else
            {
                _velocity += Time.fixedDeltaTime * new Vector3(0, -_gravity, 0);
            }

            _velocity += _speed * velocityDirection * Time.fixedDeltaTime * _controllabilityRatio;
            _velocity *= _frictionRatio;

            _characterController.Move(_velocity);
            _frictionRatios = new();
            _controllabilityRatios = new();
        }

        private void OnFloorTriggerStay(Collider other)
        {
            _isJumpAvailable = true;

            Debug.Log(other.name);

            ISurface surface = other.gameObject.GetComponent<ISurface>();

            if (surface == null)
            {
                _frictionRatios.Add(_defaultSurfaceFriction);
                _controllabilityRatios.Add(_defaultSurfaceControllability);
            }
            else
            {
                _frictionRatios.Add(surface.GetFrictionRatio());
                _controllabilityRatios.Add(surface.GetControlabilityRatio());
            }
        }

    }
}
