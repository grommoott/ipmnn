using UnityEngine;
using TMPro;
using Player;
using Global.GameObjects;

namespace Helpers
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Transform _markerChildren;
        [SerializeField] private float _scaleMultiplier;
        [SerializeField] private float _defaultScale;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetLabel(string text)
        {
            _label.text = text;
        }

        private void Update()
        {
            _markerChildren.LookAt(PlayerController.Instance.transform);
            _markerChildren.localEulerAngles = new Vector3(0, _markerChildren.localEulerAngles.y, 0);

            float scale =
                _scaleMultiplier * (Vector3.Distance(_markerChildren.position, PlayerController.Instance.transform.position))
                + _defaultScale;

            _markerChildren.localScale = new Vector3(scale, scale, scale);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public static Marker Spawn(Vector3 position, string text)
        {
            Marker result = GameObjectsManager.Instance.Spawn(GameObjectsManager.Instance.Marker.gameObject).GetComponent<Marker>();
            result.SetPosition(position);
            result.SetLabel(text);

            return result;
        }
    }
}
