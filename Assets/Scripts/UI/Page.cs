using UnityEngine;

namespace UI
{
    public abstract class Page : MonoBehaviour
    {
        protected Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Hide()
        {
            _animator.Play("Hide");
        }

        public void Show()
        {
            _animator.Play("Show");
        }
    }
}
