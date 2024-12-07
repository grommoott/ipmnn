using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Quests.Tasks;

namespace Quests.List
{
    public static class BasicQuests
    {
        public static readonly Dictionary<string, Func<QuestState, int, JObject, Quest>> Quests = new()
        {
            {QuestIds.GoToUmartalyk, GoToUmartalyc}
        };

        private static Quest GoToUmartalyc(QuestState state, int activeTaskId, JObject initialState)
        {
            QuestTask goToUmartalyk = new GoToLocationTask(Saves.LocationNames.Urmartalyk, "Доберитесь до Умарталыка");

            Quest quest = new Quest
                (
                    QuestIds.GoToUmartalyk,
                    "Вперёд!.. В Умарталык!",
                    "Ой-ой, похоже вы попали в совершенно иной мир, населённый разумными медведями. Отправьтесь в своё первое путешествие, пройдя через портал",
                    QuestState.NotStarted,
                    new QuestRewards
                    (
                        new() { Items.ItemManager.GetById(Items.ItemIds.EnergyHoney, 10) },
                        null,
                        null
                    ),
                    new() { goToUmartalyk },
                    activeTaskId
        );

            return quest;
        }
    }
}

