using UnityEngine;

namespace GunPrototype.Math
{
    public struct ParabolaTrajectory : ITrajectory
    {
        public float ProjectileSpeed;
        public Vector3 OriginPosition;
        public Vector3 OriginDirection;
        public float Gravity;

        public ParabolaTrajectory(float projectileSpeed, float gravity, Vector3 originPosition, Vector3 originDirection)
        {
            ProjectileSpeed = projectileSpeed;
            Gravity = gravity;
            OriginPosition = originPosition;
            OriginDirection = originDirection.normalized; 
        }

        public Vector3 GetPosition(float time)
        {
            Vector3 velocity = OriginDirection * ProjectileSpeed;
            Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z); 
            float verticalVelocity = velocity.y;

            Vector3 horizontalPosition = OriginPosition + horizontalVelocity * time; 
            float verticalPosition = OriginPosition.y + (verticalVelocity * time) + (0.5f * Gravity * time * time); 

            return new Vector3(horizontalPosition.x, verticalPosition, horizontalPosition.z);
        }
    }
}