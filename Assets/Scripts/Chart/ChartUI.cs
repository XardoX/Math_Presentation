using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MathPresentation
{
    using Methods;
    using System;
    using UnityEngine.UI;

    public class ChartUI : MonoBehaviour
    {
        public Action onPreviousClicked, onNextClicked;

        [SerializeField]
        private CanvasGroup methodInfo;

        [SerializeField]
        private TextMeshProUGUI methodTitleText,
            methodDescriptionText,
            sliderValueText,
            sliderDescriptionText;
        [SerializeField]
        private RectTransform sliderParent;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private Button previousMethodButton, nextMethodButton;

        public void SetMethodText(Method method)
        {
            methodInfo.alpha = 1f;
            methodTitleText.text = method.Title;
            methodDescriptionText.text = method.Description;
        }

        public void HideMethodText(Method method)
        {
            methodInfo.alpha = 0f;
        }

        public Slider SetSlider(float value, float min = 0f, float max = 1f, string description = "")
        {
            sliderParent.gameObject.SetActive(true);
            slider.minValue = min;
            slider.maxValue = max;
            slider.SetValueWithoutNotify(value);
            sliderValueText.text = value.ToString("0.00");
            sliderDescriptionText.text = description;
            return slider;
        }

        public void UpdateSliderText(float value)
        {
            sliderValueText.text = value.ToString("0.00");
        }

        public void HideSlider()
        {
            sliderParent.gameObject.SetActive(false);
        }

        private void Awake()
        {
            slider.onValueChanged.AddListener(UpdateSliderText);
            previousMethodButton.onClick.AddListener(() => onPreviousClicked?.Invoke());
            nextMethodButton.onClick.AddListener(() => onNextClicked?.Invoke());
        }
    }
}