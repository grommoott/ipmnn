using Items.Interfaces;

namespace Items.Classes.InteractingTools
{
    public class Net : InteractingTool
    {
        override protected float GetPreparingTime() => 0.1f;
        override protected float GetCooldownTime() => 0.4f;
        public Net(int count) : base(count, ItemIds.Net, "Сачок", "Хороший инструмент для ловли пчёл") { }
    }
}
