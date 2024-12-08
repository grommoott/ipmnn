using System;

namespace Interacting
{
    public class Interaction
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        private InteractingMethod _method;
        public InteractingMethod Method
        {
            get
            {
                return _method;
            }
        }

        private Action<IInteractor> _onInteract;
        public Action<IInteractor> OnInteract
        {
            get
            {
                return _onInteract;
            }
        }

        private Predicate<IInteractor> _getIsAvailable;
        public Predicate<IInteractor> GetIsAvailable
        {
            get
            {
                return _getIsAvailable;
            }
        }

        public Interaction(string name, InteractingMethod method, Action<IInteractor> onInteract, Predicate<IInteractor> getIsAvailable)
        {
            _name = name;
            _method = method;
            _onInteract = onInteract;
            _getIsAvailable = getIsAvailable;
        }
    }
}
