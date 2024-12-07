using Newtonsoft.Json.Linq;

namespace Saves
{
    public interface ISaveable
    {
        public void Load(JObject data);
        public object Save();
        public string GetSavingPath();
    }
}
