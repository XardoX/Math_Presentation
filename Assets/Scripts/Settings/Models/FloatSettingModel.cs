using UnityEngine;

namespace MathPresentation.Settings
{
    public class FloatSettingModel : SettingModel, ISetting<float>
    {
        [SerializeField]
        private float value;

        public virtual float GetValue()
        {
            return value;
        }

        public virtual void SetValue(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}

