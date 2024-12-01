using UnityEngine;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _headBone;

        public float HeadRotation;
        public bool IsMoving = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            _headBone.localEulerAngles = new Vector3(HeadRotation, 0, 0);
        }

        private void Update()
        {
            if (IsMoving)
            {
                _animator.Play("Walk");
            }
            else
            {
                _animator.Play("Idle");
            }
        }
    }
}
