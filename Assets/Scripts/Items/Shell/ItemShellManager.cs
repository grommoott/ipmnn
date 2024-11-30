using UnityEngine;
using System.Collections.Generic;

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

        [SerializeField] private ItemShell itemShells = new();
    }
}
