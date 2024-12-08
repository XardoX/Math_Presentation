using Codice.Client.BaseCommands.Ls;
using MyBox;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace MathPresentation.Settings
{
    [CreateAssetMenu(fileName = "LanguageSetting", menuName = "Data/Settings/Language")]
    public class LanguageSetting : IntSettingModel
    {
        [SerializeField]
        private List<Locale> locales;

        [SerializeField]
        private List<string> languages;

        public override string[] GetAllPossibleValues()
        {
            return languages.ToArray();
        }

        [ButtonMethod]
        private void OnEnable()
        {
            languages = LocalizationSettings.Instance.GetAvailableLocales().Locales
                .Select(locale => locale.LocaleName)
                .ToList();
            locales = LocalizationSettings.Instance.GetAvailableLocales().Locales;
            max = locales.Count-1;
        }

        protected override void OnValueChanged(int oldValue)
        {
            LocalizationSettings.Instance.SetSelectedLocale(locales[value]);
        }
    }
}