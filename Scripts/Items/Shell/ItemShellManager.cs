using UnityEngine;

namespace Items.Shell
{
    public class ItemShellManager : MonoBehaviour
    {
        private static ItemShellManager _instance;
        public static ItemShellManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogError("ItemShellManager instance isn't setted");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        [SerializeField] private GameObject ItemShellGO;

        public void SpawnItem(Item item, Vector3 position)
        {
            GameObject itemShellGO = Instantiate(ItemShellGO, position, Quaternion.identity, transform);
            ItemShell itemShell = itemShellGO.AddComponent<ItemShell>();
            itemShell.SetItem(item);
        }
    }
}
