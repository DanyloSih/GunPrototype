using UnityEngine;

namespace GunPrototype.Weapons
{
    public class WeaponUsingInputBinder : MonoBehaviour
    {
        [SerializeField] private KeyCode _useWeaponKey;
        [SerializeField] private WeaponHolder _weaponHolder;

        public void Update()
        {
            if (Input.GetKey(_useWeaponKey))
            {
                Weapon weapon = _weaponHolder.GetWeapon();
                weapon?.Use();
            }
        }
    }
}