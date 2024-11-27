using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MathPresentation.Extenstions;

namespace MathPresentation.UI.Components
{
    public class SliderValue : MonoBehaviour
    {
        [SerializeField]
        public Slider slider;

        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private bool percentageValue;

        [SerializeField]
        private bool remapValue;

        [SerializeField]
        private float _outMin = 0f;
        [SerializeField]
        private float _outMax = 100f;
        private void Awake()
        {
            if (slider == null)
            {
                slider = GetComponent<Slider>();
            }
            // slider.onValueChanged.RemoveAllListeners();
            if (text == null)
            {
                text = GetComponentInChildren<TextMeshProUGUI>();
            }
            slider.onValueChanged.AddListener(delegate { UpdateSliderText(); });
        }

        public void UpdateSliderText()
        {
            if (remapValue)
            {
                text.text = slider.value.RemapValue(slider.minValue, slider.maxValue, _outMin, _outMax).ToString("0");
            }
            else
            {
                text.text = slider.value.ToString("0");
            }

            if (percentageValue)
            {
                text.text = text.text + "%";
            }
        }
        public void SetValue(float value)
        {
            slider.value = value;
            UpdateSliderText();
        }
    }
}