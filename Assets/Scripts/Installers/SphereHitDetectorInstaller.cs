using GunPrototype.Weapons;
using UnityEngine;
using Zenject;

namespace GunPrototype.Installers
{
    public class SphereHitDetectorInstaller : MonoInstaller
    {
        [SerializeField] private SphereHitDetector.Config _sphereHitDetectorConfig;

        public override void InstallBindings()
        {
            Container.Bind<IHitDetector>().To<SphereHitDetector>()
                .AsSingle().WithArguments(_sphereHitDetectorConfig);
        }
    }
}