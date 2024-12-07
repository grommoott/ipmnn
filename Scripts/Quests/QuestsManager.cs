using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace Quests
{
    public static class QuestsManager
    {
        private static Dictionary<string, Func<QuestState, int, JObject, Quest>> __quests = null;
        private static Dictionary<string, Func<QuestState, int, JObject, Quest>> _quests
        {
            get
            {
                if (__quests == null)
                {
                    __quests = new();

                    foreach (KeyValuePair<string, Func<QuestState, int, JObject, Quest>> pair in List.TestQuests.Quests)
                    {
                        __quests.Add(pair.Key, pair.Value);
                    }

                    foreach (KeyValuePair<string, Func<QuestState, int, JObject, Quest>> pair in List.BasicQuests.Quests)
                    {
                        __quests.Add(pair.Key, pair.Value);
                    }
                }

                return __quests;
            }
        }

        public static Quest GetById(string id)
        {
            return GetById(QuestState.NotStarted, id, 0, null);
        }

        public static Quest GetById(QuestState state, string id, int activeTaskIndex, JObject initialState)
        {
            Func<QuestState, int, JObject, Quest> quest = _quests[id];

            if (quest == null)
            {
                return null;
            }

            return quest.Invoke(state, activeTaskIndex, initialState);
        }
    }
}
