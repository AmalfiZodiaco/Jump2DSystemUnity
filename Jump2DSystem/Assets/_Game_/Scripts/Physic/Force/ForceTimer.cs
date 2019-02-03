using Pearl.Clock;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Demo
{
    public class ForceTimer : Force
    {
        protected Timer timer;

        public ForceTimer(Vector2 force, string name, float timeDuration) : base(force, name)
        {
            this.timer = new Timer(timeDuration);
        }

        public override Vector2 GetForce()
        {
            return force;
        }

        public sealed override bool IsTerminated()
        {
            return timer.IsFinish();
        }
    }
}

