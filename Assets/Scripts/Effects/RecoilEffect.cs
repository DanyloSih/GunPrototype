using System;
using System.Collections;
using AbstractDefence.Utilities;
using UnityEngine;

namespace AbstractDefence.Effects
{
    public class RecoilEffect : Effect
    {
        [Serializable]
        public class Config
        {
            public Vector3 Force;
            public AnimationCurve Curve;
        }

        private Config _config;
        private Transform _recoilingTransform;
        private Vector3 _initialLocalPosition;

        public RecoilEffect(Config config, Transform recoilingTransform)
        {
            _config = config;
            _recoilingTransform = recoilingTransform;
        }

        protected override IEnumerator OnStart(float effectForce)
        {
            float timer = 0;
            float duration = _config.Curve.GetDuration();
            _initialLocalPosition = _recoilingTransform.localPosition;

            while (timer < duration)
            {
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
                float progress = _config.Curve.Evaluate(timer);
                _recoilingTransform.localPosition = _initialLocalPosition + _config.Force * progress * effectForce;
            }
        }

        protected override void OnStop()
        {
            _recoilingTransform.localPosition = _initialLocalPosition;
        }
    }
}