namespace Anchored.Util.Timing
{
    public class WaitForMilliseconds : WaitYieldInstruction
    {
        public WaitForMilliseconds(float ms)
        {
            timeRemaining = ms;
        }

        public override void Update()
        {
            timeRemaining -= (float)Time.GameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
