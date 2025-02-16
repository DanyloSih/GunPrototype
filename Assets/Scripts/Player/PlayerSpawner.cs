using System;
using UnityEngine;
using Zenject;

namespace GunPrototype.Player
{
    public class PlayerSpawner 
    {
        [Serializable]
        public class Config
        {
            public PlayerComponents PlayerPrefab;
            public Transform SpawnPoint;
            public Transform Parent;
        }

        private DiContainer _container;
        private Config _config;
        private PlayerComponents _instance;

        public PlayerComponents PlayerInstance { get => _instance; }

        public PlayerSpawner(DiContainer container, Config config)
        {
            _container = container;
            _config = config;
        }

        public PlayerComponents Respawn()
        {
            if (_instance == null)
            {
                PlayerComponents player = _container
                    .InstantiatePrefabForComponent<PlayerComponents>(_config.PlayerPrefab, _config.Parent);

                _instance = player;
            }

            _instance.transform.position = _config.SpawnPoint.position;
            _instance.transform.rotation = _config.SpawnPoint.rotation;
            return _instance;
        }
    }
}