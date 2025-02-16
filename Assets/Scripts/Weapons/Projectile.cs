using System;
using GunPrototype.Common;
using GunPrototype.Math;

namespace GunPrototype.Weapons
{
    public abstract class Projectile : PoolObject
    {
        protected float ProjectileSpeed;
        protected ITrajectory Trajectory;

        public event Action Launched;
            
        protected override void OnDisable()
        {
            base.OnDisable();
            Trajectory = null;
        }

        public void Launch(ITrajectory trajectory, float projectileSpeed)
        {          
            ProjectileSpeed = projectileSpeed;
            Trajectory = trajectory;
            Launched?.Invoke();
            OnLaunch();
        }

        protected abstract void OnLaunch();
    }
}