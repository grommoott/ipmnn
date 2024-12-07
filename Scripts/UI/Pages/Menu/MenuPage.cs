using UnityEngine.UI;
using UnityEngine;
using Global.PreferencesService;

namespace UI.Pages.Menu
{
    public class MenuPage : Page
    {
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _sensitivitySlider;

        private void Start()
        {
            _volumeSlider.value = PreferencesService.Instance.VolumeMultiplier;
            _sensitivitySlider.value = PreferencesService.Instance.SensitivityMultiplier;
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
            PreferencesService.Instance.VolumeMultiplier = _volumeSlider.value;
        }

        public void OnSensitivityChange()
        {
            PreferencesService.Instance.SensitivityMultiplier = _sensitivitySlider.value;
        }

        public static MenuPage SpawnPage()
        {
            GameObject pageGO = UIManager.Instance.InstantiatePage(UIManager.Instance.MenuPage);
            return pageGO.GetComponent<MenuPage>();
        }
    }
}
