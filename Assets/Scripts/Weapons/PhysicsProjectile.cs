using UnityEngine;
using GunPrototype.Math;
using Zenject;
using System;

namespace GunPrototype.Weapons
{
    public class PhysicsProjectile : Projectile
    {
        [Inject] private IHitDetector _hitDetector;

        [SerializeField] private int _bounceLimit = 1;
        [SerializeField] private float _bounceSpeedMultiplier = 0.5f;      

        private float _timer = 0;
        private Vector3 _previousPosition;
        private int _bounceCount;

        public Vector3 Velocity { get; private set; }
        public int BounceCount { get => _bounceCount; }

        public event Action<Hit> Hit;
        public event Action Explode;

        protected override void Update()
        {
            base.Update();

            if (Trajectory != null && ReturnToPoolCallback != null)
            {
                _timer += Time.deltaTime;
                Velocity = transform.position - _previousPosition;
                _previousPosition = transform.position;
                transform.position = Trajectory.GetPosition(_timer);

                Vector3 point2 = Trajectory.GetPosition(_timer + Time.deltaTime);
                if (_hitDetector.TryHit(_previousPosition, point2, out var hit))
                {
                    OnHit(hit);
                }
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _timer = 0;
            _bounceCount = 0;
            _previousPosition = transform.position;
        }

        protected override void OnLaunch()
        {
            _timer = 0;
            _bounceCount = 0;
            _previousPosition = transform.position;
            transform.position = Trajectory.GetPosition(0);
        }

        private void OnHit(Hit hit)
        {
            _bounceCount++;
            Hit?.Invoke(hit);
            ProjectileSpeed *= _bounceSpeedMultiplier;

            Trajectory = new ParabolaTrajectory(
                ProjectileSpeed, 
                Physics.gravity.y, 
                transform.position, 
                Vector3.Reflect(Velocity, hit.Normal));

            _timer = 0;

            if (_bounceCount > _bounceLimit)
            {
                Explode?.Invoke();
                ReturnToPoolCallback.Invoke(this);
            }
        }
    }
}