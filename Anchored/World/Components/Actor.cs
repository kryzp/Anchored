using System.Collections;
using System.Collections.Generic;
using Anchored.Util.Timing;

namespace Anchored.World.Components
{
    public abstract class Actor : Component, IUpdatable
    {
        protected List<Coroutine> coroutines = new List<Coroutine>();

        public abstract int Order { get; set; }

        public virtual void Update()
        {
            UpdateCoroutines();
        }
        
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            var cr = new Coroutine(routine);
            coroutines.Add(cr);
            return cr;
        }
        
        private void UpdateCoroutines()
        {
            coroutines.RemoveAll(c => c.IsFinished);
            var coroutinesToUpdate = coroutines.ToArray();
            foreach (var coroutine in coroutinesToUpdate)
                coroutine.Update();
        }
    }
}
