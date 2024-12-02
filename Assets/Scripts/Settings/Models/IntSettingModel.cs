using UnityEngine;
namespace MathPresentation.Settings
{
    public class IntSettingModel : SettingModel, ISetting<int>
    {
        [SerializeField]
        private int value;

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int value)
        {
            this.value = value;
        }
    }
}