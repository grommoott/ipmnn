using System;

namespace Serialization
{
    [Serializable]
    public class Location
    {
        public string LocationName;

        [Newtonsoft.Json.JsonConstructor]
        public Location(string locationName)
        {
            LocationName = locationName;
        }
    }

}
