namespace Player
{
    public class PlayerAnimation
    {
        private int _hash;
        public int Hash
        {
            get
            {
                return _hash;
            }
        }

        private int _layer;
        public int Layer
        {
            get
            {
                return _layer;
            }
        }

        private float _transitionDuration;
        public float TransitionDuration
        {
            get
            {
                return _transitionDuration;
            }
        }

        public PlayerAnimation(int hash, int layer, float transitionDuration)
        {
            _hash = hash;
            _layer = layer;
            _transitionDuration = transitionDuration;
        }
    }
}
