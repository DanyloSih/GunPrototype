using UnityEngine;

namespace AbstractDefence.Utilities
{
    public static class AnimationCurveExtensions
    {
        public static float GetDuration(this AnimationCurve curve)
        {
            return curve.keys.Length == 0 ? 0 : curve.keys[curve.keys.Length - 1].time;   
        }
    }
}