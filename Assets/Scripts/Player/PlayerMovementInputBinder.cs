using UnityEngine;

namespace GunPrototype.Player
{
    public class PlayerMovementInputBinder : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        public void Update()
        {
            Vector2 movementVector = new Vector2(Input.GetAxis("HorizontalFlat"), Input.GetAxis("VerticalFlat"));

            _playerMovement.PlayerRotator.Rotate(movementVector * Time.deltaTime);
        }
    }
}