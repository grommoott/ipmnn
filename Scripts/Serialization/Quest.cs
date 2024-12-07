using System;

namespace Serialization
{
    [Serializable]
    public class Quest
    {
        public string Id;
        public int ActiveTaskIndex;
        public Task ActiveTask;
        public Quests.QuestState State;

        [Newtonsoft.Json.JsonConstructor]
        public Quest(string id, int activeTaskIndex, Task activeTask, Quests.QuestState state)
        {
            Id = id;
            ActiveTaskIndex = activeTaskIndex;
            ActiveTask = activeTask;
            State = state;
        }
    }
}
