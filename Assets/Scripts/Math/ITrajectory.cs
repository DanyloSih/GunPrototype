using UnityEngine;

namespace GunPrototype.Math
{
    public interface ITrajectory
    {
        public Vector3 GetPosition(float time);
    }
}