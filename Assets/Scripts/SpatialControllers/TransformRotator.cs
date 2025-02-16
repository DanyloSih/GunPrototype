using System;
using UnityEngine;

namespace GunPrototype.SpatialControllers
{
    public class TransformRotator : IObjectRotator
    {
        [Serializable]
        public class Config
        {
            public float XSensitivity;
            public float YSensitivity;
            [Tooltip("In angles.")]
            public Vector2 XAxisMinMaxLimit;
        }

        private Config _config;
        private Transform _xAxisRotationTransform;
        private Transform _yAxisRotationTransform;
        private Vector2 _currentRotation;

        public TransformRotator(Config config, Transform xAxisRotationTransform, Transform yAxisRotationTransform)
        {
            _config = config;

            _xAxisRotationTransform = xAxisRotationTransform;
            _yAxisRotationTransform = yAxisRotationTransform;

            _currentRotation = new Vector2(
                _xAxisRotationTransform.localEulerAngles.x,
                _yAxisRotationTransform.localEulerAngles.y);
        }

        public void Rotate(Vector2 rotationDelta)
        {
            _currentRotation.y += rotationDelta.x * _config.XSensitivity;
            _currentRotation.x -= rotationDelta.y * _config.YSensitivity;

            _currentRotation.x = Mathf.Clamp(_currentRotation.x, _config.XAxisMinMaxLimit.x, _config.XAxisMinMaxLimit.y);

            _yAxisRotationTransform.localRotation = Quaternion.Euler(0, _currentRotation.y, 0);
            _xAxisRotationTransform.localRotation = Quaternion.Euler(_currentRotation.x, 0, 0);
        }
    }
}