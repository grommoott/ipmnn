using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Global.GlobalEvents;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.ObjectModel;

namespace Saves
{
    public class SavesManager : MonoBehaviour
    {
        private static SavesManager _instance = null;
        public static SavesManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("SavesManager instance isn't setted");
                }

                return _instance;
            }
        }

        private static string _playerName = null;
        public static string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; _isGameStateInitialized = false; _gameState = null; }
        }

        [SerializeField] private List<GameObject> _saveablesGO;
        private List<ISaveable> _saveables;

        private static string _saveFolderPath
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, "Saves");
            }
        }
        private static string _saveFilePath
        {
            get
            {
                byte[] bytes;
                string fileName = ".json";

                if (_playerName != null)
                {

                    bytes = System.Text.Encoding.UTF8.GetBytes(PlayerName);
                    fileName = Convert.ToBase64String(bytes) + ".json";
                }

                return Path.Combine
                    (
                        _saveFolderPath,
                        fileName
                    );
            }
        }

        private static bool _isSaveFileBusy = false;
        private static int _busyFilePollingDelay = 25;

        private bool _isGameLoaded = false;
        public bool IsGameLoaded { get { return _isGameLoaded; } }
        private static Dictionary<string, JObject> _gameState;
        private static bool _isGameStateInitialized;

        private static Dictionary<string, int> _sceneIds = new() { { LocationNames.Hub, 2 }, { LocationNames.Urmartalyk, 3 } };
        public static ReadOnlyDictionary<string, int> SceneIds
        {
            get
            {
                return new ReadOnlyDictionary<string, int>(_sceneIds);
            }
        }

        private static string _currentLocationName;
        public static string CurrentLocationName { get { return _currentLocationName; } }

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
            }

            _instance = this;
            _saveables = new();

            if (!_isGameStateInitialized)
            {
                _gameState = new();
            }

            _currentLocationName = GetCurrentLocationName();
        }

        private void Start()
        {
            foreach (GameObject go in _saveablesGO)
            {
                ISaveable[] saveables = go.GetComponents<ISaveable>();

                if (saveables == null)
                {
                    Debug.LogError($"{go.name} isn't implements ISaveable");
                }

                foreach (ISaveable saveable in saveables)
                {
                    _saveables.Add(saveable);
                }
            }

            if (!_isGameStateInitialized)
            {
                foreach (ISaveable saveable in _saveables)
                {
                    _gameState.Add(saveable.GetSavingPath(), null);
                }

            }

            LoadGame();
        }

        private async void OnDestroy()
        {
            await SaveGame();
        }

        private async void OnApplicationQuit()
        {
            await SaveGame();
        }

        private string GetCurrentLocationName()
        {
            return _sceneIds.FirstOrDefault(scene => scene.Value == SceneManager.GetActiveScene().buildIndex).Key;
        }

        private async Task SaveState()
        {
            if (_playerName == null || !_isGameStateInitialized)
            {
                return;
            }

            _gameState["location"] = JObject.FromObject
                (
                    new Serialization.Location(GetCurrentLocationName())
                );
            string saveString = Serialization.Json.Serialize(_gameState);

            if (!File.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }

            if (!File.Exists(_saveFilePath))
            {
                FileStream fs = File.Create(_saveFilePath);
                fs.Close();
            }

            while (_isSaveFileBusy)
            {
                await Task.Delay(_busyFilePollingDelay);
            }

            _isSaveFileBusy = true;
            await File.WriteAllTextAsync(_saveFilePath, saveString, System.Text.Encoding.UTF8);
            _isSaveFileBusy = false;
        }

        public void SaveField(ISaveable saveable)
        {
            _gameState[saveable.GetSavingPath()] = JObject.FromObject(saveable.Save());

            SaveState();
        }

        public async Task SaveGame()
        {
            foreach (ISaveable saveable in _saveables)
            {
                if (saveable == null)
                {
                    Debug.LogError("Saveable is null at SavesManager.SaveGame.foreach.if");
                    return;
                }

                _gameState[saveable.GetSavingPath()] = JObject.FromObject(saveable.Save());
            }

            await SaveState();
        }

        private async void LoadGame()
        {
            if (_playerName == null)
            {
                return;
            }

            if (_isGameStateInitialized)
            {
                foreach (ISaveable saveable in _saveables)
                {
                    saveable.Load(_gameState.GetValueOrDefault(saveable.GetSavingPath(), null) as JObject);
                }

                _isGameLoaded = true;
                GlobalEventsManager.Instance.Game.OnLoad.Invoke();
                return;
            }

            if (!File.Exists(_saveFilePath))
            {
                _isGameLoaded = true;
                _isGameStateInitialized = true;

                foreach (ISaveable saveable in _saveables)
                {
                    saveable.Load(null);
                }

                GlobalEventsManager.Instance.Game.OnLoad.Invoke();
                return;
            }

            while (_isSaveFileBusy)
            {
                await Task.Delay(_busyFilePollingDelay);
            }

            _isSaveFileBusy = true;
            string saveString = await File.ReadAllTextAsync(_saveFilePath);
            _isSaveFileBusy = false;

            _gameState = Serialization.Json.Parse<Dictionary<string, JObject>>(saveString);
            _isGameStateInitialized = true;

            if (_gameState == null)
            {
                foreach (ISaveable saveable in _saveables)
                {
                    saveable.Load(null);
                }

                _gameState = new();
            }
            else
            {
                foreach (ISaveable saveable in _saveables)
                {
                    saveable.Load(_gameState.GetValueOrDefault(saveable.GetSavingPath(), null) as JObject);
                }
            }

            _isGameLoaded = true;
            GlobalEventsManager.Instance.Game.OnLoad.Invoke();
        }

        public static async void GoToScene(string sceneName)
        {
            if (sceneName == null)
            {
                GoToScene(LocationNames.Hub);
                return;
            }

            await _instance?.SaveGame();
            _instance = null;
            await SceneManager.LoadSceneAsync(_sceneIds[sceneName]);
        }

        public async static void LoadMainScene()
        {
            if (!File.Exists(_saveFilePath))
            {
                GoToScene(LocationNames.Hub);
                return;
            }

            string saveString = await File.ReadAllTextAsync(_saveFilePath);
            _gameState = Serialization.Json.Parse<Dictionary<string, JObject>>(saveString);
            _isGameStateInitialized = true;

            if (_gameState == null)
            {
                GoToScene(LocationNames.Hub);
                return;
            }
            else
            {
                string sceneName = _gameState["location"].ToObject<Serialization.Location>().LocationName;

                GoToScene(sceneName);
            }
        }

        public static async void ToMainMenu()
        {
            await _instance?.SaveGame();
            _instance = null;
            await SceneManager.LoadSceneAsync(0);
        }
    }
}
