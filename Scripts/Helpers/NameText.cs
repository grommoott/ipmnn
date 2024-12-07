using UnityEngine;
using TMPro;

namespace Helpers
{
    public class NameText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        private Transform _target;

        private void Start()
        {
            _target = Player.PlayerController.Instance.transform;
        }

        private void LateUpdate()
        {
            _nameText.transform.LookAt(_target, Vector3.up);
        }

        public void SetText(string text)
        {
            _nameText.text = text;
        }
    }
}
