using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using Quests.Tasks;
using UnityEngine;
using Items;

namespace Quests.List
{
    public static class BasicQuests
    {
        public static readonly Dictionary<string, Func<QuestState, int, JObject, Quest>> Quests = new()
        {
            {QuestIds.GoToUmartalyk, GoToUmartalyc},
            {QuestIds.SdelayEnergoPchely, SdelayEnergoPchely},
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

        private static Quest SdelayEnergoPchely(QuestState state, int activeTaskId, JObject initialState)
        {
            QuestTask goToPolyana = new GoToMarkerTask("Прийти на поляну", new Vector3(830, 1.9f, 447), "Поляна", 10);

            List<Item> bees = new() { ItemManager.GetById(ItemIds.Bee, 10) };

            QuestTask collectBees = new CollectItemsTask("Собрать 10 пчёл", bees);
            QuestTask talkToBakset = new TalkToTask("Поговорить с Баксером");

            Quest quest = new Quest
                (
                QuestIds.SdelayEnergoPchely,
                "Энергопчёлы - будущее",
                "Соберите 10 пчёл с поляны, расположенной неподалёку для Баксера",
                QuestState.NotStarted,
                new QuestRewards
                (
                    new() { ItemManager.GetById(Items.ItemIds.Honey, 30) },
                    null,
                    null
                ),
                new() { goToPolyana, collectBees, talkToBakset },
                activeTaskId
                );

            return quest;
        }
    }
}

