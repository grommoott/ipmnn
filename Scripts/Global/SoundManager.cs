using UnityEngine;
using System.Collections.Generic;
using Global.Preferences;

namespace Global.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("SoundManager instance isn't setted");
                }

                return _instance;
            }
        }

        [SerializeField] private List<AudioSource> _backgroundSounds;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Update()
        {
            SetBackgroundVolume(PreferencesManager.Instance.Volume);
        }

        private void SetBackgroundVolume(float volume)
        {
            foreach (AudioSource source in _backgroundSounds)
            {
                source.volume = volume;
            }
        }
    }
}
