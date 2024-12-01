using UnityEngine;
using Helpers;
using System.Collections.Generic;
using Global.InputManager;
using Global.PreferencesService;

namespace Player
{
    public class PlayerMovementManager : MonoBehaviour
    {
        private PlayerController _player;
        public PlayerController Player
        {
            get
            {
                return _player;
            }
        }

        private CharacterController _characterController;
        private LayerMask _surface;

        [SerializeField] private Transform _head;
        [SerializeField] private Transform _floorPoint;
        [SerializeField] private float _floorOverlapRadius;
        [SerializeField] private LayerMask _floorOverlapMask;
        [SerializeField] private float _airFriction;
        [SerializeField] private float _airControllability;
        [SerializeField] private float _defaultSurfaceFriction;
        [SerializeField] private float _defaultSurfaceControllability;
        [SerializeField] private float _speed;
        [SerializeField] private float _sprintingSpeed;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpVelocity;
        [SerializeField] private MinMax _cameraExtremumRotations;

        private Vector3 _velocity;
        private float _headRotation;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _headRotation += InputManager.Instance.CameraMove.y * PreferencesService.Instance.Sensitivity * Time.deltaTime;
            transform.Rotate(new Vector3(0, InputManager.Instance.CameraMove.x, 0) * PreferencesService.Instance.Sensitivity * Time.deltaTime);

            _headRotation = Mathf.Clamp(_headRotation, _cameraExtremumRotations.Min, _cameraExtremumRotations.Max);

            _head.localEulerAngles = new Vector3(_headRotation, 0, 0);
            Player.AnimationManager.HeadRotation = _headRotation;
        }

        private void FixedUpdate()
        {
            (float _frictionRatio, float _controllabilityRatio) = GetFrictionAndControllability();

            Vector2 axes = InputManager.Instance.Axes;
            Vector3 velocityDirection = transform.localToWorldMatrix.MultiplyVector(new Vector3(axes.x, 0, axes.y));

            Player.AnimationManager.IsMoving = Vector3.Distance(axes, Vector3.zero) != 0;

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

            float speed = InputManager.Instance.IsSprinting ? _sprintingSpeed : _speed;

            _velocity += velocityDirection * Time.fixedDeltaTime * _controllabilityRatio * speed;
            _velocity *= _frictionRatio;

            _characterController.Move(_velocity);
        }

        private (float, float) GetFrictionAndControllability()
        {
            Collider[] colliders = Physics.OverlapSphere(_floorPoint.position, _floorOverlapRadius, _floorOverlapMask);

            List<float> _frictionRatios = new();
            List<float> _controllabilityRatios = new();

            _frictionRatios.Add(_airFriction);
            _controllabilityRatios.Add(_airControllability);

            foreach (Collider collider in colliders)
            {
                ISurface surface = collider.gameObject.GetComponent<ISurface>();

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

            float _frictionRatio = 0;
            _frictionRatios.ForEach(ratio => _frictionRatio += ratio);
            _frictionRatio /= _frictionRatios.Count;

            float _controllabilityRatio = 0;
            _controllabilityRatios.ForEach(ratio => _controllabilityRatio += ratio);
            _controllabilityRatio /= _controllabilityRatios.Count;

            return (_frictionRatio, _controllabilityRatio);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_floorPoint.position, _floorOverlapRadius);
        }

    }
}
