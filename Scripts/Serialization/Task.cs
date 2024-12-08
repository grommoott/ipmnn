using System;
using Newtonsoft.Json.Linq;

namespace Serialization
{
    [Serializable]
    public class Task
    {
        public JObject State;

        [Newtonsoft.Json.JsonConstructor]
        public Task(JObject state)
        {
            State = state;
        }
    }
}
