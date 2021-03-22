namespace Anchored.Util.Timing
{
    public class WaitForSeconds : WaitYieldInstruction
    {
        public WaitForSeconds(float sec)
        {
            timeRemaining = sec;
        }

        public override void Update()
        {
            timeRemaining -= (float)Time.GameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
