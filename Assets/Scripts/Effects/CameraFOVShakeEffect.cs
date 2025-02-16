using System;
using System.Collections;
using AbstractDefence.Utilities;
using UnityEngine;

namespace AbstractDefence.Effects
{
    public class CameraFOVShakeEffect : Effect
    {
        [Serializable]
        public class Config
        {
            public AnimationCurve Curve;
        }

        private float _initialFOV;
        private Config _config;
        private Camera _camera;

        public CameraFOVShakeEffect(Config config, Camera camera)
        {
            _camera = camera;
            _config = config;          
        }

        protected override IEnumerator OnStart(float effectForce)
        {
            float timer = 0;
            float duration = _config.Curve.GetDuration();
            _initialFOV = _camera.fieldOfView;

            while (timer < duration)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                _camera.fieldOfView = _initialFOV + _config.Curve.Evaluate(timer) * effectForce;
            }
        }

        protected override void OnStop()
        {
            _camera.fieldOfView = _initialFOV;
        }
    }
}