using MathPresentation.DialogSystem;
using MathPresentation.LocalizationWrapper;
using MyBox;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;

namespace MathPresentation.DialogSystem
{
    [System.Serializable]
    public class DialogLine : Model
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
        [ConditionalField(nameof(key), true)]
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

        public string Line
        {
            get
            {
                if(line.IsNullOrEmpty()) return englishLine;
                return line;
            }
        }

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

        public bool ManualEdit { get => manualEdit; set => manualEdit = value; }
        public string Key => key;

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

        public void SetKey(string key)
        {
            this.key = key;
            name = string.Join(" ", key.Split('_').TakeLast(2));
        }
#endif
    }
}