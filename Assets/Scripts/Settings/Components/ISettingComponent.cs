using UnityEngine;
namespace MathPresentation.Settings.Components
{
    public interface ISettingComponent<T>
    {
        public abstract void LoadSetting();

        public abstract void SetSetting(T value);
    }
}