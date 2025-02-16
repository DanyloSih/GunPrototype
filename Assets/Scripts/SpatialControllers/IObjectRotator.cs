using UnityEngine;

namespace GunPrototype.SpatialControllers
{
    public interface IObjectRotator
    {
        public void Rotate(Vector2 rotationDelta);
    }
}