using UnityEngine;
namespace MathPresentation.Settings
{
    public abstract class IntSettingModel : SettingModel
    {
        [SerializeField]
        protected int value;
        [SerializeField]
        protected int min;
        [SerializeField]
        protected int max;

        public int Min => min;
        public int Max => max;

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int value)
        {
            this.value = Mathf.Clamp(value, min, max);
            OnValueChanged(value);
        }
        
        protected virtual void OnValueChanged(int oldValue)
        {
        }
    }
}