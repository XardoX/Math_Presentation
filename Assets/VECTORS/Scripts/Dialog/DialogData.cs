using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using System.Linq;
using UnityEditor;
using Newtonsoft.Json;
using MyBox;
using MathPresentation.LocalizationWrapper;

namespace MathPresentation.DialogSystem
{
    public enum DialogType
    {
        Main,
        Stage,
        Background
    }

    [CreateAssetMenu(fileName = "DialogData", menuName = "Data/Dialog", order = 1)]
    public class DialogData : Model
    {
        [SerializeField]
        private string tableName = "Dialogs";

        [SerializeField]
        private string charactersPath = "Assets/Data";

        [SerializeField]
        private DialogCharacter[] characters;

        [SerializeField]
        private List<DialogLine> dialog = new();

        [SerializeField]
        private Sprite background;

        private DialogType dialogType;

        public DialogCharacter[] Characters => characters;

        public List<DialogLine> Dialog => dialog;

        public Sprite Background => background;


        private void OnEnable()
        {
#if UNITY_EDITOR

            ImportLinesFromLocalization();
#endif
            Localization.LocaleChanged += LoadLines;
        }

        private void LoadLines(Locale locale) => LoadLines();

        public void LoadLines()
        {
            for (int i = 0; i < dialog.Count; i++)
            {
                dialog[i].LoadLine();
            }
        }

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();
            var prefix = name.Split("_").First();

            switch (prefix)
            {
                case "MD":
                    dialogType = DialogType.Main;
                    break;
                case "BGD":
                    dialogType = DialogType.Background;
                    break;
            }

            ImportLinesFromLocalization();
            SetKeyForManualLines();
        }

        [ButtonMethod]
        public void AddNewLine()
        {
            string newKey = $"{id}_L{dialog.Count +1}_";
            var newLine = CreateInstance<DialogLine>();
            newLine.name = $"DialogLine_{dialog.Count}";

            Localization.AddEntry(tableName, newKey, string.Empty);

            newLine.ImportLine(tableName, newKey);
            AssetDatabase.AddObjectToAsset(newLine, this);

            dialog.Add(newLine);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        [ButtonMethod]
        public void RemoveLastLine()
        {
            if (dialog.Count <= 0) return;

            var lastLine = dialog[dialog.Count - 1];
            Localization.RemoveEntry(tableName, lastLine.Key);
            dialog.RemoveAt(dialog.Count - 1);
            DestroyImmediate(lastLine, true);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }

        private void SetKeyForManualLines()
        {
            for (int i = 0; i < dialog.Count; i++)
            {
                if (dialog[i].ManualEdit)
                {
                    var characterName = dialog[i].SpeakingCharacter != null ? dialog[i].SpeakingCharacter.ID : "";
                    dialog[i].SetKey($"{id}_L{i + 1}_{characterName}");

                }
            }
        }

        /// <summary>
        /// Creates lines from keys from localization table
        /// </summary>
        [ButtonMethod]
        private void CreateLinesFromLocalization()
        {
            var dialogLinesKeys = Localization.GetDialogLines(tableName, id);
            for (int i = 0; i < dialogLinesKeys.Count; i++)
            {
                if (dialog.Count <= i)
                {
                    dialog.Add(new DialogLine());
                }
                if (dialog[i] == null)
                {
                    dialog[i] = new DialogLine();
                }
                dialog[i].ImportLine(tableName, dialogLinesKeys[i]);
            }
            if (dialog.Count > dialogLinesKeys.Count)
            {
                dialog.RemoveRange(dialogLinesKeys.Count, dialog.Count - dialogLinesKeys.Count);
            }
        }

        /// <summary>
        /// Imports lines based on keays from localization
        /// </summary>
        [ButtonMethod]
        private void ImportLinesFromLocalization()
        {
            var dialogLinesKeys = Localization.GetDialogLines(tableName, id);
            for (int i = 0; i < dialogLinesKeys.Count; i++)
            {
                dialog[i].ImportLine(tableName, dialogLinesKeys[i]);
            }
        }

        /// <summary>
        /// Creates localization keys from custom lines in inspector
        /// </summary>
        [ButtonMethod]
        private void CreateLocalizationKeys()
        {

        }

        [ButtonMethod]
        
        private void AutoFillCharacters()
        {
            if (characters.Length < 2) return; 
            foreach (var line in dialog)
            {
                line.ImportCharacters(characters[0], characters[1]);
            }
        }

 
#endif
    }
}