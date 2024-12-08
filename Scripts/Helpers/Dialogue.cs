using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Helpers
{
    public class Dialogue
    {
        private List<string> _replics;
        public ReadOnlyCollection<string> Replics { get { return _replics.AsReadOnly(); } }

        private string _companionName;
        public string CompanionName { get { return _companionName; } }

        private Action _onDialogueEnd;
        public void OnDialogueEnd()
        {
            if (_onDialogueEnd == null)
            {
                return;
            }

            _onDialogueEnd.Invoke();
        }

        public Dialogue(List<string> replics, string companionName, Action onDialogueEnd)
        {
            _replics = replics;
            _companionName = companionName;
            _onDialogueEnd = onDialogueEnd;
        }
    }
}
