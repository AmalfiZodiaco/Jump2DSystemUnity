using Pearl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Demo
{
    public class ForceDegenerateTimer : ForceTimer
    {
        protected Range rangeX;
        protected Range rangeY;

        public ForceDegenerateTimer(Vector2 force, string name, float timeDuration) : base(force, name, timeDuration)
        {
            if (force.x > 0)
                rangeX = new Range(0, force.x);
            else
                rangeX = new Range(force.x, 0);

            if (force.y > 0)
                rangeY = new Range(0, force.y);
            else
                rangeY = new Range(force.y, 0);
        }

        public override Vector2 GetForce()
        {
            force.x = MathfExtend.ChangeRange(timer.TimeInPercentReversed, rangeX);
            force.y = MathfExtend.ChangeRange(timer.TimeInPercentReversed, rangeY);
            return force;
        }
    }
}
