using UnityEngine;
using Saves;
using System.Collections;

namespace Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        class PlayerAnimationState
        {
            public bool IsMoving = false;
        }

        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _animatorWithoutHead;
        [SerializeField] private Transform _headBone;

        private PlayerAnimationState _state = new PlayerAnimationState();

        public float HeadRotation;
        public bool IsMoving = false;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);
        }

        public void PlayAnimation(PlayerAnimation animation)
        {
            _animator.CrossFade(animation.Hash, animation.TransitionDuration, animation.Layer);
            _animatorWithoutHead.CrossFade(animation.Hash, animation.TransitionDuration, animation.Layer);
        }

        private void LateUpdate()
        {
            _headBone.localEulerAngles = new Vector3(HeadRotation, 0, 0);
        }

        private void Update()
        {
            if (IsMoving == _state.IsMoving)
            {
                return;
            }

            PlayAnimation(IsMoving ? PlayerAnimations.Walk : PlayerAnimations.Idle);
            _state.IsMoving = IsMoving;
        }
    }
}
