using Helpers;
using Saves;
using Player;
using Quests;
using System.Linq;
using Quests;

namespace Interacting.Interactables.NPSs
{
    public class Guide : NPC
    {
        public override string GetName() => "Координатор";

        public override Dialogue GetDialogue()
        {
            string name = SavesManager.PlayerName;

            if (PlayerController.Instance.QuestManager.CompletedQuestIds.Contains(QuestIds.GoToUmartalyk))
            {
                return new
                    (
                     new()
                     {
                     $"Хмм-ммм... привет, х-ммм, {name}"
                     },
                     GetName(),
                     () => { }
                     );
            }

            if (PlayerController.Instance.QuestManager.Quests.Keys.Contains(QuestIds.GoToUmartalyk))
            {
                return new
                    (
                        new()
                    {
                        "Х-ммм, я уже где-то видел тебя раньше",
                        $"Аххх-ммм, да, точно ты же {name}",
                        "Если хочешь получить мёда, то отправляйся в Урмарталык. Червоточина прямо позади, хммм... тебя"
                    },
                        GetName(),
                        () => { }
                    );
            }

            return new
            (
                new()
                {
                    "Привет!",
                    "Хммм... не видел тебя здесь раньше, х-хмм... похоже ты новенький",
                    $"Тебя ведь зовут хммм... {name}, правильно?",
                    "Что ж, хм-хмм я полагаю ты хочешь узнать побольше об этом мире, так что я готов рассказать тебе о нём",
                    "Основной, хммм... валютой в нашем мире является, ххм.. мёд. Его также используют как, хммм... пищу",
                    "Немного об, хм... управлении. Для того, чтобы закрыть любое окно нажми, хммм... \"Escape\", для открытия инвентаря - \"E\", для окна квестов, хммм... \"Q\"",
                    "Хммм... c некторыми предметами можно взаимодействовать. Доступные способы взаимодействия располагаются чуть ниже, хммм... центра экрана",
                    "Доступные способы взаимодействия могут также зависeть от, хммм... инструмента который ты держишь в руке",
                    "Хммм... больше ничего в голову не лезет, что я бы, хммм... мог тебе рассказать",
                    "Я даю тебе твоё первое, хммм... задание. Ты, хм..., должен пройти через кротовую нору и, хмм... попасть на планету Умарталык",
                    "Хмм... Умарталык - это экономический центр, хммм... мира, здесь производится куча видов мёда. Там ты, хммм... легко сможешь заработать денег на первое время"
                },
                GetName(),
                () =>
                {
                    Player.PlayerController.Instance.QuestManager.AddQuest(Quests.QuestsManager.GetById(Quests.QuestIds.GoToUmartalyk));
                }
            );
        }
    }
}
