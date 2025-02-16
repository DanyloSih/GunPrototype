using GunPrototype.Player;
using GunPrototype.Weapons;
using UnityEngine;
using Zenject;

namespace GunPrototype.Bootstrap
{
    public class MainSceneBootstrapper : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private PlayerSpawner _playerSpawner;

        [SerializeField] private Weapon _initialWeaponPrefab;

        protected void Start()
        {
            PlayerComponents player = _playerSpawner.Respawn();
            Weapon weapon = _container
                .InstantiatePrefabForComponent<Weapon>(_initialWeaponPrefab);

            player.WeaponHolder.SetWeapon(weapon);
        }
    }
}