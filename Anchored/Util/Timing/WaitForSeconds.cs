namespace Anchored.Util.Timing
{
    public class WaitForSeconds : YieldInstruction
    {
        private float timeRemaining;

        public bool IsFinished => timeRemaining <= 0f;

        public WaitForSeconds(float sec)
        {
            timeRemaining = sec;
        }

        public void Update()
        {
            timeRemaining -= (float)Time.GameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
