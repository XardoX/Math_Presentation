using UnityEngine;

namespace MathPresentation.Settings
{
    public abstract class SettingComponent<C,T> : MonoBehaviour
    {
        [SerializeField]
        private SettingsData data;

        [SerializeField]
        private C component;

        public abstract void LoadSetting();

        public abstract void SetSetting(T value);

        protected virtual void GetUIComponent()
        {
            component = GetComponentInChildren<C>(true);
        }

        protected virtual void Awake()
        {
            GetUIComponent();
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }
    }
}
