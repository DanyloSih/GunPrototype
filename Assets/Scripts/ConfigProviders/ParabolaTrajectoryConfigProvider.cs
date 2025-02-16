using GunPrototype.Math;
using UnityEngine;
using UnityEngine.UI;

namespace GunPrototype.ConfigProviders
{
    public class ParabolaTrajectoryConfigProvider : ConfigProvider<ParabolaTrajectoryFactory.Config>
    {
        [SerializeField] private Slider _projectileSpeedSlider;

        private ParabolaTrajectoryFactory.Config _config;

        private ParabolaTrajectoryFactory.Config Config { get => _config = _config ?? new(); }

        protected void OnEnable()
        {
            _projectileSpeedSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        protected void OnDisable()
        {
            _projectileSpeedSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        public override ParabolaTrajectoryFactory.Config GetConfig()
        {
            ParabolaTrajectoryFactory.Config config = Config;

            UpdateConfig(config);

            return config;
        }

        private void UpdateConfig(ParabolaTrajectoryFactory.Config config)
        {
            config.ProjectileSpeed = _projectileSpeedSlider.value;
            config.Gravity = Physics.gravity.y;
        }

        private void OnSliderValueChanged(float value)
        {
            UpdateConfig(Config);
        }   
    }
}