using MathPresentation.UI.Components;
using UnityEngine;
using UnityEngine.UI;
namespace MathPresentation.Settings.Components
{
    public class SliderSetting : SettingComponent<Slider, FloatSettingModel>
    {
        [SerializeField]
        private SliderValue sliderValue;

        public void LoadSetting()
        {
            component.SetValueWithoutNotify(setting.GetValue());
        }

        public void SetSetting(float value)
        {
            setting.SetValue(value);
        }

        private void OnEnable()
        {
            component.onValueChanged.AddListener(SetSetting);
            LoadSetting();
        }

        private void OnDisable()
        {
            component.onValueChanged.RemoveListener(SetSetting);
        }

        protected override void Awake()
        {
            base.Awake();
            if(sliderValue == null)
            {
                sliderValue = GetComponentInChildren<SliderValue>(true);
            }
        }
    }
}