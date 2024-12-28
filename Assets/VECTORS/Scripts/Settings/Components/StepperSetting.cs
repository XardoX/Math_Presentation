using UnityEngine;
using UnityEngine.UI;
namespace MathPresentation.Settings.Components
{
    public class StepperSetting : SettingComponent<Stepper, IntSettingModel>, ISettingComponent<int>
    {
        private string[] values;

        public void LoadSetting()
        {
            component.value = setting.GetValue();
            component.Text.text = values[setting.GetValue()];
        }

        public void SetSetting(int value)
        {
            setting.SetValue(value);
            component.Text.text = values[value];
        }

        private void OnEnable()
        {
            component.onValueChanged.AddListener(SetSetting);
            values = setting.GetAllPossibleValues();
            component.maximum = setting.Max;
            LoadSetting();
        }

        private void OnDisable()
        {
            component.onValueChanged.RemoveListener(SetSetting);
        }

    }
}
