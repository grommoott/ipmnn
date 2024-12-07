namespace Effects
{
    public class EffectBuffs
    {
        public float SpeedBuff { get; private set; }
        public float JumpBuff { get; private set; }

        public EffectBuffs Multiply(float multiplier)
        {
            return new EffectBuffs(SpeedBuff * multiplier, JumpBuff * multiplier);
        }

        public EffectBuffs Add(EffectBuffs other)
        {
            return new EffectBuffs(SpeedBuff + other.SpeedBuff, JumpBuff + other.JumpBuff);
        }

        public EffectBuffs(float speedBuff = 1, float jumpBuff = 1)
        {
            SpeedBuff = speedBuff;
            JumpBuff = jumpBuff;
        }
    }
}
