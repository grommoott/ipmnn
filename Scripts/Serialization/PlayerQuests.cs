using System;
using System.Linq;
using System.Collections.Generic;

namespace Serialization
{
    [Serializable]
    public class PlayerQuests
    {
        public List<Quest> Quests;
        public List<string> CompletedQuestIds;
        public string ActiveQuestId;

        [Newtonsoft.Json.JsonConstructor]
        public PlayerQuests(List<Quest> quests, List<string> completedQuestIds, string activeQuestId)
        {
            Quests = quests;
            CompletedQuestIds = completedQuestIds;
            ActiveQuestId = activeQuestId;
        }

        public PlayerQuests(Dictionary<string, Quests.Quest> quests, List<string> completedQuestIds, string activeQuestId)
        {
            Quests = quests.Values
                .Select((quest) =>
                        new Quest
                            (
                                quest.Id,
                                quest.ActiveTaskIndex,
                                new Task(quest.ActiveTask.GetState()),
                                quest.State
                            )
                        )
                .ToList();

            CompletedQuestIds = completedQuestIds;
            ActiveQuestId = activeQuestId;
        }
    }
}
