using System;
using UnityEngine;

namespace Serialization
{
    [Serializable]
    public class PlayerMovement
    {
        public Vector Position;
        public Vector EulerAngles;
        public string LocationName;

        [Newtonsoft.Json.JsonConstructor]
        public PlayerMovement(Vector position, Vector eulerAngles, string locationName)
        {
            Position = position;
            EulerAngles = eulerAngles;
            LocationName = locationName;
        }

        public PlayerMovement(Vector3 position, Vector3 eulerAngles, string locationName)
        {
            Position = position;
            EulerAngles = eulerAngles;
            LocationName = locationName;
        }
    }
}
