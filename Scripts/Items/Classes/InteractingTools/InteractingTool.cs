using Items.Interfaces;
using System.Collections;
using UnityEngine;
using System;
using Items.Storages;

namespace Items.Classes.InteractingTools
{
    public abstract class InteractingTool : Item, IInteractingTool
    {
        private bool _isBusy = false;
        public bool IsBusy { get { return _isBusy; } }

        public void InteractingToolUse(Action callback)
        {
            Player.PlayerController.Instance.InteractingManager.StartCoroutine(MakeDelay(callback));
        }

        public PlayerInventorySlot GetSlot()
        {
            return PlayerInventorySlot.Hand;
        }

        virtual public void Equip() { }
        virtual public void Unequip() { }

        protected abstract float GetCooldownTime();
        protected abstract float GetPreparingTime();

        private IEnumerator MakeDelay(Action callback)
        {
            if (_isBusy)
            {
                yield break;
            }

            _isBusy = true;
            Player.PlayerController.Instance.AnimationManager.PlayAnimation(Player.PlayerAnimations.Use);
            yield return new WaitForSeconds(GetPreparingTime());

            if (callback != null)
            {
                callback.Invoke();
            }

            yield return new WaitForSeconds(GetCooldownTime());
            _isBusy = false;
        }

        public InteractingTool(int count, string id, string name, string description) : base(count, id, name, description) { }
    }
}
