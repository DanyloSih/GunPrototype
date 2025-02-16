using UnityEngine;
using System;

namespace GunPrototype.Weapons
{
    public class SphereHitDetector : IHitDetector
    {
        [Serializable]
        public class Config
        {
            public float Radius;
        }

        private Config _config;

        public SphereHitDetector(Config config)
        {
            _config = config;
        }

        public bool TryHit(Vector3 point1, Vector3 point2, out Hit hit)
        {
            Vector3 direction = point2 - point1;
            float distance = Vector3.Distance(point1, point2);

            if (Physics.SphereCast(
                new Ray(point1, direction), _config.Radius, out var hitInfo, distance))
            {
                hit = new Hit() 
                { 
                    Normal = hitInfo.normal, 
                    Point = hitInfo.point, 
                    UV = hitInfo.textureCoord, 
                    Collider = hitInfo.collider 
                };

                return true;
            }

            hit = default;
            return false;
        }
    }
}