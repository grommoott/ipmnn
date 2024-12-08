using Helpers;
using Player;
using Quests;

namespace Interacting.Interactables.NPSs
{
    public class Bakser : NPC
    {
        override public Dialogue GetDialogue()
        {
            if (PlayerController.Instance.QuestManager.CompletedQuestIds.Contains(""))
            {
                return new Dialogue
                    (
                        new()
                        {
                            "Пасиба!",
                            "Thank you!!",
                        },
                        GetName(),
                        () => { }
                    );
            }

            if (PlayerController.Instance.QuestManager.Quests.ContainsKey(QuestIds.SdelayEnergoPchely))
            {
                Quest quest = PlayerController.Instance.QuestManager.Quests[QuestIds.SdelayEnergoPchely];

                if (quest.ActiveTaskIndex == 2)
                {
                    quest.ActiveTask.OnComplete();

                    return new Dialogue
                        (
                            new()
                            {
                    "Огромное спасибо!",
                            },
                            GetName(),
                            () => { }
                        );
                }
                else
                {
                    return new Dialogue
                        (
                            new()
                            {
                        "Ты уже принёс их?"
                            },
                            GetName(),
                            () => { }
                        );
                }
            }

            return new Dialogue
                (
                    new()
                    {
                        "Совсем недавно я вычитал легенду про то что раньше были пчёлы которые питались энергомёдом и не умирали как обычные, но из-за того что 300 лет назад упал метеорит все они были уничтожены",
                        "Мне нужны эти пчёлы. Не спрашивай зачем",
                        "Могу ли я попросить тебя принести 10 пчёл с поляны, расположенной неподалёку. Конечно же за награду",
                        "Пчёл можно поймать с помощью сачка, который продаётся в магазине за 10 мёда",
                        "Удачи!"
                    },
                    GetName(),
                    () =>
                    {
                        PlayerController.Instance.QuestManager.AddQuest(QuestsManager.GetById(QuestIds.SdelayEnergoPchely));
                    }
                );
        }

        override public string GetName() => "Баксёр";
    }
}
