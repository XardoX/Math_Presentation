using UnityEngine;

namespace MathPresentation.Settings
{
    public class FloatSettingModel : SettingModel
    {
        [SerializeField]
        protected float value, 
            min = 0f, 
            max = 99999f;

        public float Min => min;
        public float Max => max;

        public virtual float GetValue()
        {
            return value;
        }

        public virtual void SetValue(float value)
        {
            OnValueChanged(this.value);
            this.value = Mathf.Clamp(value, min, max);
        }

        protected virtual void OnValueChanged(float oldValue)
        {
        }
    }
}

