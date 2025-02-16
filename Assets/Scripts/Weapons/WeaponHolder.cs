using UnityEngine;

namespace GunPrototype.Weapons
{
    public class WeaponHolder : MonoBehaviour
    {
        private Weapon _currentWeapon;

        public void SetWeapon(Weapon weapon)
        {
            Deactivate(weapon);
            _currentWeapon = weapon;
            Activate(weapon);
        }

        public Weapon GetWeapon()
        {
            return _currentWeapon;
        }

        private void Activate(Weapon weapon)
        {
            weapon.transform.position = transform.position;
            weapon.transform.parent = transform;
            weapon.gameObject.SetActive(true);
        }

        private static void Deactivate(Weapon weapon)
        {
            weapon.transform.parent = null;
            weapon.gameObject.SetActive(false);
        }
    }
}