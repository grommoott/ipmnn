using System;

namespace Items.Interfaces
{
    public interface IInteractingTool : IEquipable
    {
        public void InteractingToolUse(Action callback);
    }
}
