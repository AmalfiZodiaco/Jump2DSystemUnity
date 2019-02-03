using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Demo
{
    public class ForceDegenerateCurveTimer : ForceDegenerateTimer
    {
        private AnimationCurve curveX;
        private AnimationCurve curveY;

        public ForceDegenerateCurveTimer(Vector2 force, string name, float timeDuration, AnimationCurve curveX, AnimationCurve curveY) : base(force, name, timeDuration)
        {
            this.curveX = curveX;
            this.curveY = curveY;
        }

        public override Vector2 GetForce()
        {
            if (curveX != null)
                force.x = MathfExtend.ChangeRange(curveX.Evaluate(timer.TimeInPercent), rangeX);
            if (curveY != null)
                force.y = MathfExtend.ChangeRange(curveY.Evaluate(timer.TimeInPercent), rangeY);
            return force;
        }
    }
}
