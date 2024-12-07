using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Helpers
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderForeground;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Color _color;

        private void Start()
        {
            _sliderForeground.color = _color;
        }

        public void SetValue(float value)
        {
            _valueText.text = value.ToString("0");
            _slider.value = value;
        }

        public void SetMinMax(MinMax minMax)
        {
            _slider.minValue = minMax.Min;
            _slider.maxValue = minMax.Max;
        }
    }
}

