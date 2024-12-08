using UnityEngine;
using Helpers;
using Player;

namespace UI.Pages.Sleeping
{
    public class SleepingPage : Page
    {
        [SerializeField] private Bar _stamineBar;
        [SerializeField] private Bar _saturationBar;

        private void Start()
        {
            _stamineBar.SetMinMax(PlayerController.Instance.StateManager.StamineMinMax);
            _saturationBar.SetMinMax(PlayerController.Instance.StateManager.SaturationMinMax);
        }

        public void Update()
        {
            _stamineBar.SetValue(PlayerController.Instance.StateManager.Stamine);
            _saturationBar.SetValue(PlayerController.Instance.StateManager.Saturation);
        }

        public void WakeUp()
        {
            PlayerController.Instance.SleepingManager.WakeUp();
        }

        public static SleepingPage SpawnPage()
        {
            GameObject pageGO = UIManager.Instance.InstantiatePage(UIManager.Instance.SleepingPage);
            return pageGO.GetComponent<SleepingPage>();
        }
    }
}
