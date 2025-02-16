using System;
using UnityEngine;

namespace GunPrototype.SpatialControllers
{
    public class CharacterControllerMover : IObjectMover
    {
        [Serializable]
        public class Config
        {
            public float MovementSpeed;
        }

        private Config _config;
        private CharacterController _characterController;
        private Transform _directionAnchor;

        public CharacterControllerMover(Config config, CharacterController characterController, Transform directionAnchor = null)
        {
            _characterController = characterController;
            _directionAnchor = directionAnchor;
            _config = config;
        }

        public void Move(Vector2 moveDirection)
        {
            Vector3 transformedDirection = new Vector3(moveDirection.x, 0, moveDirection.y);

            transformedDirection = _directionAnchor
                ? _directionAnchor.TransformDirection(transformedDirection)
                : transformedDirection;

            _characterController.Move(transformedDirection * _config.MovementSpeed);
        }
    }
}