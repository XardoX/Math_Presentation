using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.Localization;
#endif
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace MathPresentation.LocalizationWrapper
{
    public class Localization
    {
        public static void Init()
        {
            LocalizationSettings.InitializationOperation
                .Completed += _ => Debug.Log("Initialization Completed");
        }

        public static event Action<Locale> LocaleChanged
        {
            add => LocalizationSettings.Instance.OnSelectedLocaleChanged += value;
            remove => LocalizationSettings.Instance.OnSelectedLocaleChanged -= value;
        }

        private static T GetLocalizedAsset<T>(string table, string entry)
            where T : UnityEngine.Object =>
            LocalizationSettings.AssetDatabase.GetLocalizedAsset<T>(table, entry);

        private static string GetLocalizedString(string table, string entry) =>
            LocalizationSettings.StringDatabase.GetLocalizedString(table, entry);

        private static string GetLocalizedString(string table, string entry, params object[] arguments) =>
            LocalizationSettings.StringDatabase.GetLocalizedString(table, entry, arguments);

#if UNITY_EDITOR

        public static List<string> GetDialogLines(string table, string key)
        {
            var rowList = LocalizationEditorSettings.GetStringTableCollection(table).GetRowEnumerator().Where(_ => _.KeyEntry.Key.Contains(key));
            List<string> lineKeys = new List<string>();
            foreach (var row in rowList)
            {
                lineKeys.Add(row.KeyEntry.Key);
            }
            return lineKeys;
        }

        public static string GetLocalizedStringInEditor(string table, string entry) =>
            LocalizationEditorSettings.GetStringTableCollection(table).GetRowEnumerator().First(_ => _.KeyEntry.Key == entry).TableEntries[0].LocalizedValue;

        public static TableEntryReference GetTableEntryReference(string tableId, string entryKey)
        {
            var table = LocalizationEditorSettings.GetStringTableCollection(tableId);
            if (table == null) return null;

            var entry = table.SharedData.GetEntry(entryKey);
            return entry != null
            ? entry.Id
            : default;
        }

        public static TableReference GetTableReference(string tableCollectionName)
        {
            var tableCollection = LocalizationEditorSettings.GetStringTableCollection(tableCollectionName);
            if (tableCollection == null)
                return default;

            // Use implicit conversion from string
            return tableCollectionName;
        }
#endif

        public static void SetLocale(int index)
        {
            var locales = GetAvailableLocales();

            if (index < locales.Count)
            {
                SetLocale(locales[index]);
            }
        }

        public static void SetLocale(Locale locale) =>
            LocalizationSettings.Instance.SetSelectedLocale(locale);

        public static Locale GetSelectedLocale() =>
            LocalizationSettings.Instance.GetSelectedLocale();

        public static List<Locale> GetAvailableLocales() =>
            LocalizationSettings.Instance.GetAvailableLocales().Locales;

        public static string GetDefault(string entry)
        {
#if UNITY_EDITOR
            return GetLocalizedStringInEditor("Main", entry);
#else
            return GetLocalizedString("Main", entry);
#endif
        }

        public static string GetVectors(string entry)
        {
#if UNITY_EDITOR
            return GetLocalizedStringInEditor("Vectors", entry);
#else
            return GetLocalizedString("Vectors", entry);
#endif
        }
    }
}