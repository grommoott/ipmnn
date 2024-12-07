using System;
using Global.Preferences;

namespace Serialization
{
    [Serializable]
    public class Preferences
    {
        public float SensitivityMultiplier = 1;
        public float VolumeMultiplier = 1;

        [Newtonsoft.Json.JsonConstructor]
        public Preferences(float sensitivityMultiplier, float volumeMultiplier)
        {
            SensitivityMultiplier = sensitivityMultiplier;
            VolumeMultiplier = volumeMultiplier;
        }

        public Preferences(PreferencesManager preferences)
        {
            SensitivityMultiplier = preferences.SensitivityMultiplier;
            VolumeMultiplier = preferences.VolumeMultiplier;
        }
    }
}
