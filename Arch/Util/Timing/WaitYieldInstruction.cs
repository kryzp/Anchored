namespace Arch.Util.Timing
{
    public abstract class WaitYieldInstruction
    {
        protected float timeRemaining;
        public bool IsFinished => timeRemaining <= 0f;

        public abstract void Update();
    }
}
