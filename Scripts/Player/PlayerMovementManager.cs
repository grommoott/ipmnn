using UnityEngine;
using Helpers;
using System.Collections.Generic;
using Global.InputManager;
using Global.PreferencesService;
using Effects;
using System.Collections;
using Saves;
using UI;

namespace Player
{
    public class PlayerMovementManager : MonoBehaviour, ISaveable
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        private CharacterController _characterController;
        private LayerMask _surface;

        [SerializeField] private Transform _head;
        [SerializeField] private Transform _floorPoint;
        [SerializeField] private Transform _childrenTransform;
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
        private Vector3 _position;
        private float _selfRotation;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
            _characterController = GetComponent<CharacterController>();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);
        }

        private void LateUpdate()
        {
            if (UIManager.Instance.IsPageOpended)
            {
                return;
            }

            _headRotation -= Input.GetAxis("Mouse Y") * PreferencesService.Instance.Sensitivity * Time.deltaTime;
            _childrenTransform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * PreferencesService.Instance.Sensitivity * Time.deltaTime);

            _headRotation = Mathf.Clamp(_headRotation, _cameraExtremumRotations.Min, _cameraExtremumRotations.Max);
            _selfRotation = transform.localEulerAngles.y;

            _head.localEulerAngles = new Vector3(_headRotation, 0, 0);
            Player.AnimationManager.HeadRotation = _headRotation;
        }

        private void FixedUpdate()
        {
            _position = transform.position;

            if (UIManager.Instance.IsPageOpended)
            {
                return;
            }

            (float _frictionRatio, float _controllabilityRatio) = GetFrictionAndControllability();
            EffectBuffs buffs = Player.EffectsManager.GetBuffs();

            Vector2 axes = InputManager.Instance.Axes;
            Vector3 velocityDirection = _childrenTransform.localToWorldMatrix.MultiplyVector(new Vector3(axes.x, 0, axes.y));

            Player.AnimationManager.IsMoving = Vector3.Distance(axes, Vector3.zero) != 0;

            if (_characterController.isGrounded)
            {
                _velocity.y = 0;

                if (InputManager.Instance.IsJump)
                {
                    _velocity.y = _jumpVelocity * buffs.JumpBuff;
                }
            }
            else
            {
                _velocity += Time.fixedDeltaTime * new Vector3(0, -_gravity, 0);
            }

            float speed = buffs.SpeedBuff * (InputManager.Instance.IsSprinting ? _sprintingSpeed : _speed);

            _velocity += velocityDirection * Time.fixedDeltaTime * _controllabilityRatio * speed;
            _velocity *= _frictionRatio;

            _characterController.Move(_velocity);

            _position = transform.position;
        }

        private (float, float) GetFrictionAndControllability()
        {
            if (!_characterController.isGrounded)
            {
                return (_airFriction, _airControllability);
            }

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

        public void Load(Newtonsoft.Json.Linq.JObject data)
        {
            if (data == null)
            {
                return;
            }

            Serialization.PlayerMovement movement = data.ToObject<Serialization.PlayerMovement>();

            if (SavesManager.CurrentLocationName != movement.LocationName)
            {
                return;
            }


            // without detectCollisions = false player teleports to saved position only for 1 frame and after goes to spawn
            _characterController.enabled = false;
            transform.Translate(movement.Position - transform.position);
            _characterController.enabled = true;

            transform.eulerAngles = new Vector3(0, movement.EulerAngles.Y, 0);
            _headRotation = movement.EulerAngles.X;
        }

        public object Save()
        {
            Debug.Log(_position);
            Serialization.PlayerMovement movement = new Serialization.PlayerMovement
                (
                    _position,
                    new Vector3(_headRotation, _selfRotation, 0),
                    SavesManager.CurrentLocationName
                );

            return movement;
        }

        public string GetSavingPath() => "playerMovement";
    }
}
