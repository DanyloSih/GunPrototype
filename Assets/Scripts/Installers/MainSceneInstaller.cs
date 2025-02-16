using GunPrototype.ConfigProviders;
using GunPrototype.Math;
using GunPrototype.Player;
using GunPrototype.Common;
using UnityEngine;
using Zenject;

namespace GunPrototype.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private ConfigProvider<ParabolaTrajectoryFactory.Config> _parabolaTrajectoryConfigProvider;
        [SerializeField] private PlayerSpawner.Config _playerSpawnerConfig;
        [SerializeField] private PoolsManager.Config _poolsManagerConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_parabolaTrajectoryConfigProvider.GetConfig()).AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle().WithArguments(_playerSpawnerConfig);
            Container.Bind<PoolsManager>().AsSingle().WithArguments(_poolsManagerConfig);
        }
    }
}