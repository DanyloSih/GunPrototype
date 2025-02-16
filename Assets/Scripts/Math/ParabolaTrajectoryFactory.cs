using System;
using GunPrototype.Common;
using UnityEngine;

namespace GunPrototype.Math
{
    public class ParabolaTrajectoryFactory : IFactory<ITrajectory>
    {
        [Serializable]
        public class Config
        {
            public float ProjectileSpeed;
            public float Gravity;
        }

        private Config _config;
        private Transform _origin;

        public ParabolaTrajectoryFactory(Config config, Transform origin)
        {
            _config = config;
            _origin = origin;
        }

        public ITrajectory Create()
        {
            return new ParabolaTrajectory(_config.ProjectileSpeed, _config.Gravity, _origin.position, _origin.forward);
        }
    }
}