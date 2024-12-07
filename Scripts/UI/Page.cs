using UnityEngine;

namespace UI
{
    public abstract class Page : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        [SerializeField] private string _pageName;
        public string PageName
        {
            get
            {
                return _pageName;
            }
        }

        private void Start()
        {
            Show();
        }

        public void Hide() // Hides page
        {
            _animator.Play("Hide");
        }

        public void Show() // Shows page
        {
            _animator.Play("Show");
        }

        public void Close() // Hides page and then destroys it
        {
            _animator.Play("Close");
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
