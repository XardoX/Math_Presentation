using UnityEngine;

namespace MathPresentation.Settings
{
    public abstract class SettingModel : ScriptableObject
    {
        [SerializeField]
        private bool enable = true;

        protected bool Enabled { get => enable; set => enable = value; }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            
        }
#endif
    }
}
