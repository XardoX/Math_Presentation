using MyBox;
using UnityEngine;

namespace MathPresentation.Settings
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Data/Settings/SettingsData", order = 0)]
    public class SettingsData : ScriptableObject
    {
        [SerializeField]
        [DisplayInspector]
        private SettingModel[] models;
    }
}