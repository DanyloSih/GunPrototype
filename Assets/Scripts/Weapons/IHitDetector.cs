using UnityEngine;

namespace GunPrototype.Weapons
{

    public interface IHitDetector
    {
        public bool TryHit(Vector3 point1, Vector3 point2, out Hit hit);
    }
}