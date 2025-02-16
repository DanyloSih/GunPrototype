using System;
using System.Diagnostics;
using GunPrototype.Common;
using GunPrototype.Math;
using UnityEngine;
using Zenject;

namespace GunPrototype.Weapons
{
    public class Cannon : Weapon
    {
        [Inject] private PoolsManager _poolsManager;
        [Inject] private ParabolaTrajectoryFactory.Config _trajectoryFactoryConfig;

        [SerializeField] private float _cooldown;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private LineRendererTrajectorySetter.Config _trajectorySetterConfig;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _muzzle;

        private Stopwatch _timer;
        private LineRendererTrajectorySetter _lineTrajectorySetter;
        private ParabolaTrajectoryFactory _parabolaTrajectoryFactory;

        public event Action Shot;

        public void Awake()
        {
            _parabolaTrajectoryFactory = new ParabolaTrajectoryFactory(
                _trajectoryFactoryConfig, _muzzle);

            _lineTrajectorySetter = new LineRendererTrajectorySetter(
                _trajectorySetterConfig, _lineRenderer);

            _timer = Stopwatch.StartNew();
        }

        protected void Update()
        {
            _lineTrajectorySetter.SetTrajectory(_parabolaTrajectoryFactory.Create());
        }

        public override void Use()
        {
            if (_timer.Elapsed.TotalSeconds > _cooldown)
            {
                Shoot();
                _timer.Restart();
            }
        }

        protected void Shoot()
        {
            UnityEngine.Debug.Log("Cannon shot!");
            Projectile projectile = _poolsManager.GetPool(_projectilePrefab).Get();
            projectile.Launch(
                _parabolaTrajectoryFactory.Create(), 
                _trajectoryFactoryConfig.ProjectileSpeed);

            Shot?.Invoke();
        }
    }
}