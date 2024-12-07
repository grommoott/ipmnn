using UnityEngine;
using UI;

namespace Player
{
    public class PlayerSleepingManager : MonoBehaviour
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        [SerializeField] private float _maxStamineToSleep;
        public float MaxStamineToSleep { get { return _maxStamineToSleep; } }

        private bool _isSleeping;
        public bool IsSleeping { get { return _isSleeping; } }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        public void Sleep()
        {
            if (Player.StateManager.Stamine > MaxStamineToSleep)
            {
                return;
            }

            _isSleeping = true;
            /*UIManager.Instance.OpenPage();*/
        }
    }
}
