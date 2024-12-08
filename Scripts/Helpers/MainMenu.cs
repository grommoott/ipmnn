using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Helpers
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _usernameInput;
        [SerializeField] private Image _usernameInputBG;
        private string _username = "";

        private void Start()
        {
            _usernameInput.onValueChanged.AddListener(OnUsernameChange);
        }

        private void OnUsernameChange(string value)
        {
            _username = value;
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void LoadGame()
        {
            if (_username == "" || _username == null)
            {
                _usernameInputBG.color = Color.red;
                return;
            }

            Saves.SavesManager.PlayerName = _username;
            Saves.SavesManager.LoadMainScene();
        }
    }
}
