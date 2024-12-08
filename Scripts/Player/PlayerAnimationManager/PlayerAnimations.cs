using UnityEngine;

namespace Player
{
    public static class PlayerAnimations
    {
        public static int LayersCount { get; private set; } = 1;

        public static PlayerAnimation Idle { get; private set; } =
            new PlayerAnimation(Animator.StringToHash("Idle"), 0, 0.2f);
        public static PlayerAnimation Walk { get; private set; } =
            new PlayerAnimation(Animator.StringToHash("Walk"), 0, 0.2f);
        public static PlayerAnimation Use { get; private set; } =
            new PlayerAnimation(Animator.StringToHash("Use"), 1, 0.05f);
    }
}
