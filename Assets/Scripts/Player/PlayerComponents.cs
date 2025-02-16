using GunPrototype.Weapons;
using UnityEngine;

namespace GunPrototype.Player
{
    public class PlayerComponents : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private WeaponHolder _weaponHolder;

        public PlayerMovement PlayerMovement { get => _playerMovement; }
        public WeaponHolder WeaponHolder { get => _weaponHolder; }
    }
}