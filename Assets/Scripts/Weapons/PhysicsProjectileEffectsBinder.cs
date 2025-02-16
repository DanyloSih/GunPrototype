using GunPrototype.Common;
using GunPrototype.Decals;
using UnityEngine;
using Zenject;

namespace GunPrototype.Weapons
{
    public class PhysicsProjectileEffectsBinder : MonoBehaviour
    {
        [Inject] private PoolsManager _poolsManager;

        [SerializeField] private PhysicsProjectile _physicsProjectile;
        [SerializeField] private MeshRandomizer.Config _meshRandomizerConfig;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private PoolObject _explodingPraticles;
        [SerializeField] private float _speedFactor = 10f;
        [SerializeField] private Vector2 _explosionScaleMinMax = new Vector2(0.5f, 0.6f);
        [SerializeField] private Color _decalBaseColor = Color.white;
        [SerializeField] private Sprite _explosionDecalSprite;


        private MeshRandomizer _meshRandomizer;

        protected void Awake()
        {
            _meshRandomizer = new MeshRandomizer(_meshRandomizerConfig, _meshFilter);
        }

        protected void OnEnable()
        {
            _physicsProjectile.Explode += OnExplode;
            _physicsProjectile.Hit += OnHit;
            _physicsProjectile.Launched += OnLaunched;
            _physicsProjectile.Timeout += OnTimeout;
        }

        protected void OnDisable()
        {
            _physicsProjectile.Explode -= OnExplode;
            _physicsProjectile.Hit -= OnHit;
            _physicsProjectile.Launched -= OnLaunched;
            _physicsProjectile.Timeout -= OnTimeout;
        }

        private void OnTimeout()
        {
            OnExplode();
        }

        private void OnLaunched()
        {
            _meshRandomizer.Randomize();
        }

        private void OnHit(Hit hit)
        {
            DecalsCanvas decalCanvas = hit.Collider.GetComponent<DecalsCanvas>();
            if (decalCanvas == null)
            {
                return;
            }

            float speedFactor = _physicsProjectile.Velocity.magnitude * _speedFactor;
            Ray ray = new Ray(transform.position, hit.Point - transform.position);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                var decal = new Decal()
                {
                    Sprite = _explosionDecalSprite,
                    Scale = Vector2.one,
                    Color = new Color(_decalBaseColor.r, _decalBaseColor.g, _decalBaseColor.b, Mathf.Clamp01(speedFactor))
                };

                decalCanvas.Print(decal, hitInfo.textureCoord);
            }        
        }

        private void OnExplode()
        {
            var explosion = _poolsManager.GetPool(_explodingPraticles).Get();
            explosion.transform.position = transform.position;
        }
    }
}