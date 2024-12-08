using UnityEngine;

namespace MathPresentation.Settings.Components
{
    public abstract class SettingComponent<C, T> : MonoBehaviour where T : SettingModel
    {
        [SerializeField]
        protected T setting;

        [SerializeField]
        protected C component;

        protected virtual void GetUIComponent()
        {
            component = GetComponentInChildren<C>(true);
        }

        protected virtual void Awake()
        {
            GetUIComponent();
        }
    }
}
