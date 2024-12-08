using UnityEngine;
using System.Collections;
using UI;

namespace Player
{
    public class PlayerSleepingManager : MonoBehaviour
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        [SerializeField] private float _maxStamineToSleep;
        public float MaxStamineToSleep { get { return _maxStamineToSleep; } }

        [SerializeField] private float _sleepingSpeed;
        [SerializeField] private float _wakeUpTime;
        [SerializeField] private float _goToSleepTime;

        private bool _isSleeping;
        public bool IsSleeping { get { return _isSleeping; } }

        private bool _isWakeUp;

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

            StartCoroutine(SleepingCoroutine());
        }

        public void WakeUp()
        {
            if (_isSleeping)
            {
                _isWakeUp = true;
            }
        }

        private IEnumerator SleepingCoroutine()
        {
            UIManager.Instance.OpenPage(UI.Pages.Sleeping.SleepingPage.SpawnPage());
            yield return new WaitForSeconds(_goToSleepTime);
            _isSleeping = true;

            while (true)
            {
                if (!_isWakeUp)
                {
                    Player.StateManager.Stamine += _sleepingSpeed * Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                else
                {
                    UIManager.Instance.ClosePage();
                    yield return new WaitForSeconds(_wakeUpTime);
                    _isSleeping = false;
                    _isWakeUp = false;
                    break;
                }
            }
        }
    }
}
