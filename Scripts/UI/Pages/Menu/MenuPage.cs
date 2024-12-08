using UnityEngine.UI;
using UnityEngine;
using Global.Preferences;

namespace UI.Pages.Menu
{
    public class MenuPage : Page
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _sensitivitySlider;

        private void Start()
        {
            _volumeSlider.value = PreferencesManager.Instance.VolumeMultiplier;
            _sensitivitySlider.value = PreferencesManager.Instance.SensitivityMultiplier;
        }

        public void Resume()
        {
            UIManager.Instance.ClosePage();
        }

        public void ToMainMenu()
        {
            Saves.SavesManager.ToMainMenu();
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void OnVolumeChange()
        {
            PreferencesManager.Instance.VolumeMultiplier = _volumeSlider.value;
        }

        public void OnSensitivityChange()
        {
            PreferencesManager.Instance.SensitivityMultiplier = _sensitivitySlider.value;
        }

        public static MenuPage SpawnPage()
        {
            GameObject pageGO = UIManager.Instance.InstantiatePage(UIManager.Instance.MenuPage);
            return pageGO.GetComponent<MenuPage>();
        }
    }
}
