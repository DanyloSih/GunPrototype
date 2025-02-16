using UnityEngine;

namespace GunPrototype.Weapons
{
    public struct Hit
    {
        public Vector3 Point;
        public Vector3 Normal;
        public Vector2 UV;
        public Collider Collider;
    }
}