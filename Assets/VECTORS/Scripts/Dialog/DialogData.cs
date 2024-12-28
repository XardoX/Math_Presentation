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

            ImportLines();
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

            ImportLines();
        }

        [ButtonMethod]
        private void CreateLines()
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

        [ButtonMethod]
        private void ImportLines()
        {
            var dialogLinesKeys = Localization.GetDialogLines(tableName, id);
            for (int i = 0; i < dialogLinesKeys.Count; i++)
            {
                dialog[i].ImportLine(tableName, dialogLinesKeys[i]);
            }
        }

        [ButtonMethod]
        private void AutoFillCharacters()
        {
            foreach (var line in dialog)
            {
                line.ImportCharacters(characters[0], characters[1]);
            }
        }
#endif
    }

    [System.Serializable]
    public class DialogLine
    {
        [SerializeField]
        [HideInInspector]
        private string name;

        [SerializeField]
        [ReadOnly]
        private string key;

        [SerializeField]
        private bool manualEdit;

        [ReadOnly(nameof(manualEdit), true)]
        [TextArea(1, 10)]
        [SerializeField]
        private string englishLine;

        [ReadOnly]
        [ConditionalField(nameof(key),true)]
        [TextArea(1, 10)]
        [SerializeField]
        private string line;

        [SerializeField]
        [ReadOnly(nameof(manualEdit), true)]
        private DialogCharacter speakingCharacter;

        [SerializeField]
        private DialogCharacter[] charactersLeft, charactersRight;

        [SerializeField]
        private Sprite overrideGraphicRight;

        [SerializeField]
        private Sprite overrideGraphicLeft;

        [SerializeField]
        [HideInInspector]
        private LocalizedString localizedString;

        public Sprite OverrideGraphicLeft => overrideGraphicLeft;
        public Sprite OverrideGraphicRight => overrideGraphicRight;

        public DialogCharacter[] CharactersLeft => charactersLeft;
        public DialogCharacter[] CharactersRight => charactersRight;

        public DialogCharacter SpeakingCharacter => speakingCharacter;

        public string Line => line;

        public int SpeakingCharacterId
        {
            get
            {
                if (charactersLeft.Length > 0)
                {
                    return charactersLeft.Contains(speakingCharacter) ? 0 : 1;

                }
                else
                {
                    return 0;
                }
            }
        }

        public async void LoadLine()
        {
            var loadAsync = localizedString.GetLocalizedStringAsync();
            await loadAsync.Task;
            if (loadAsync.IsDone)
            {
                line = loadAsync.Result;
            }
        }



#if UNITY_EDITOR
        public void ImportLine(string table, string key)
        {
            name = string.Join(" ", key.Split('_').TakeLast(2));
            this.key = key;
            englishLine = Localization.GetLocalizedStringInEditor(table, key);
            localizedString = new();

            localizedString.SetReference(table, key);

            var characterName = key.Split("_").Last();

            var character = AssetDatabase.FindAssets(characterName + " t:DialogCharacter", new[] { "Assets/VECTORS/Content/" }).FirstOrDefault();
            var path = AssetDatabase.GUIDToAssetPath(character);
            if (path != null)
            {
                speakingCharacter = (DialogCharacter)AssetDatabase.LoadAssetAtPath(path, typeof(DialogCharacter));
            }
        }

        public void ImportCharacters(DialogCharacter characterLeft, DialogCharacter characterRight)
        {
            this.charactersLeft = new DialogCharacter[] { characterLeft };
            this.charactersRight = new DialogCharacter[] { characterRight };
        }
#endif
    }
}