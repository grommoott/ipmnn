using UnityEngine;
using Helpers;

namespace Global.GameObjects
{
    public class GameObjectsManager : MonoBehaviour
    {
        private static GameObjectsManager _instance;
        public static GameObjectsManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("GameObjectsManager instance isn't setted");
                }

                return _instance;
            }
        }

        [SerializeField] private Marker _marker;
        public Marker Marker { get { return _marker; } }

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public GameObject Spawn(GameObject go)
        {
            return Instantiate(go);
        }
    }
}
