using AbstractDefence.Effects;
using GunPrototype.Common;
using GunPrototype.Math;
using UnityEngine;
using Zenject;

namespace GunPrototype.Weapons
{
    public class CannonEffectsBinder : MonoBehaviour
    {
        [Inject] private PoolsManager _poolsManager;
        [Inject] private ParabolaTrajectoryFactory.Config _parabolicTrajectoryConfig;

        [SerializeField] private Cannon _cannon;
        [SerializeField] private RecoilEffect.Config _gunRecoilEffectConfig;
        [SerializeField] private CameraFOVShakeEffect.Config _cameraFOVShakeEffectConfig;
        [SerializeField] private Transform _cannonView;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private PoolObject _muzzleFlash;
        [SerializeField] private float _projectileSpeedFactor = 0.01f;

        private RecoilEffect _gunRecoilEffect;
        private CameraFOVShakeEffect _cameraShakeEffect;

        protected void Awake()
        {
            _gunRecoilEffect = new RecoilEffect(_gunRecoilEffectConfig, _cannonView);
            _cameraShakeEffect = new CameraFOVShakeEffect(_cameraFOVShakeEffectConfig, Camera.main);
        }

        protected void OnEnable()
        {
            _cannon.Shot += OnShot;
        }

        protected void OnDisable()
        {
            _cannon.Shot -= OnShot;
        }

        private void OnShot()
        {
            var flash = _poolsManager.GetPool(_muzzleFlash).Get();
            flash.transform.position = _muzzle.transform.position;
            flash.transform.rotation = _muzzle.transform.rotation;
            float effectForce = _projectileSpeedFactor * _parabolicTrajectoryConfig.ProjectileSpeed;
            _gunRecoilEffect.Start(this, effectForce);
            _cameraShakeEffect.Start(this, effectForce);
        }
    }
}