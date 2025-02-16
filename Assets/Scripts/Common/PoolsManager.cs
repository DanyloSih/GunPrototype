using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GunPrototype.Common
{
    public class PoolsManager
    {
        [Serializable]
        public class Config
        {
            public Transform RootTransform;
        }

        private readonly Dictionary<int, object> pools = new Dictionary<int, object>();
        private readonly Dictionary<int, Transform> poolParents = new Dictionary<int, Transform>();
        private readonly Config _config;
        private readonly DiContainer _diContainer;

        public PoolsManager(Config config, DiContainer diContainer)
        {
            _config = config;
            _diContainer = diContainer;
        }

        public ObjectsPool<T> GetPool<T>(T prefab, int initialSize = 10) where T : PoolObject
        {
            int instanceID = prefab.GetInstanceID();

            if (!pools.TryGetValue(instanceID, out object pool))
            {
                GameObject poolParentObj = new GameObject(prefab.name + " Pool");
                poolParentObj.transform.parent = _config.RootTransform;

                Transform poolParent = poolParentObj.transform;
                poolParents[instanceID] = poolParent;
                pool = new ObjectsPool<T>(initialSize, _diContainer, prefab, poolParent);
                pools[instanceID] = pool;
            }

            return (ObjectsPool<T>)pool;
        }
    }
}