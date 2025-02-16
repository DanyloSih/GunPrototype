using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GunPrototype.Common
{
    public class ObjectsPool<T> where T : PoolObject
    {
        private readonly Queue<T> pool = new Queue<T>();
        private readonly T _prefab;
        private readonly Transform _poolParent;
        private readonly DiContainer _diContainer;

        public ObjectsPool(int initialSize, DiContainer diContainer, T prefab, Transform poolParent)
        {
            _prefab = prefab;
            _poolParent = poolParent;
            _diContainer = diContainer;

            for (int i = 0; i < initialSize; i++)
            {
                T obj = CreatePrefab();
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public T Get()
        {
            T obj = pool.Count > 0 ? pool.Dequeue() : CreatePrefab();
            obj.gameObject.SetActive(true);
            return obj;
        }

        private T CreatePrefab()
        {
            var prefab = _diContainer.InstantiatePrefabForComponent<T>(_prefab, _poolParent);
            prefab.ReturnToPoolCallback = (x) => Return(x as T);
            return prefab;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}