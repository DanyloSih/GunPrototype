using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GunPrototype.UI
{
    public class SliderTextBinder : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        protected void OnEnable()
        {
            OnValueChanged(_slider.value);
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _text.text = value.ToString();
        }
    }
}