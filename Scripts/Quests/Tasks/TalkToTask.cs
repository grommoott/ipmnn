using UnityEngine;
using Global.GlobalEvents;
using Helpers;

namespace Quests.Tasks
{
    public class TalkToTask : QuestTask
    {
        override public string GetProgress()
        {
            return $"В процессе";
        }

        public TalkToTask(string description) : base(description)
        {

        }
    }
}
