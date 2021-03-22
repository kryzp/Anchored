using System.Collections;

namespace Anchored.Util.Timing
{
    public class Coroutine
    {
        private IEnumerator routine;
        private WaitYieldInstruction wait;
        
        public bool IsFinished { get; set; }

        public Coroutine(IEnumerator routine)
        {
            this.routine = routine;
        }

        public void Stop()
        {
            IsFinished = true;
        }
        
        public void Update()
        {
            if (IsFinished)
                return;
            
            if (wait != null)
            {
                wait.Update();

                if (!wait.IsFinished)
                    return;
                
                wait = null;
            }

            if (!routine.MoveNext())
                IsFinished = true;
            
            wait = routine.Current as WaitYieldInstruction;
        }
    }
}
