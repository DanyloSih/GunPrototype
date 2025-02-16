using GunPrototype.SpatialControllers;
using UnityEngine;

namespace GunPrototype.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterControllerMover.Config _movingConfig;
        [SerializeField] private TransformRotator.Config _rotationConfig;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _xAxisRotatingTransform;

        private IObjectMover _playerMover;
        private IObjectRotator _playerRotator;

        public IObjectMover PlayerMover { get => _playerMover; }
        public IObjectRotator PlayerRotator { get => _playerRotator; }

        protected void Start()
        {
            _playerMover = new CharacterControllerMover(
                _movingConfig, _characterController, _characterController.transform);

            _playerRotator = new TransformRotator(
                _rotationConfig, _xAxisRotatingTransform, _characterController.transform);
        }
    }
}