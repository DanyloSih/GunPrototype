using System;
using UnityEngine;

namespace GunPrototype.Common
{
    public class PoolObject : MonoBehaviour
    {
        public Action<PoolObject> ReturnToPoolCallback;

        [SerializeField] private float _lifeDuration;

        private float _lifeTimer = 0;

        public float LifeDuration { get => _lifeDuration; }

        public event Action Timeout;

        protected virtual void Update()
        {
            if (_lifeTimer > _lifeDuration)
            {
                Timeout?.Invoke();
                ReturnToPoolCallback.Invoke(this);
            }

            _lifeTimer += Time.deltaTime;
        }

        protected virtual void OnDisable()
        {
            _lifeTimer = 0;
        }

        public void ReturnToPool()
        {
            ReturnToPoolCallback?.Invoke(this);
        }
    }
}