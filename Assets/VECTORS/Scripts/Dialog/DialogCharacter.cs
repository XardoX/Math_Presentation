using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using MathPresentation.LocalizationWrapper;
using MyBox;
using JetBrains.Annotations;


namespace MathPresentation.DialogSystem
{
    [CreateAssetMenu(fileName = "DialogCharacter", menuName = "Data/Dialog Character", order = 2)]
    [System.Serializable]
    public class DialogCharacter : Model
    {
        [SerializeField]
        [ReadOnly]
        [OverrideLabel("Character name preview")]
        private string characterName;

        [SerializeField]
        private Sprite characterGraphic;

        [SerializeField]
        private AudioClip mouthSounds;

        [SerializeField]
        [HideInInspector]
        private LocalizedString localizedName;

        [SerializeField]
        [Header("Advanced settings")]
        private bool overrideSettings = false;

        [SerializeField]
        [ReadOnly(nameof(overrideSettings), true)]
        private string tableName = "Main";

        [ReadOnly(nameof(overrideSettings), true)]
        [SerializeField]
        private string prefix = "Character_";

        [HideInInspector]
        public bool isLocalizationKeyPresent;

        public string CharacterName => localizedName.GetLocalizedString();
        public Sprite CharacterGraphic => characterGraphic;
        public AudioClip MouthSounds => mouthSounds;

#if UNITY_EDITOR
        private void OnEnable()
        {
            OnValidate();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            localizedName.SetReference(tableName, prefix + id);
            isLocalizationKeyPresent = Localization.IsKeyPresent(tableName, prefix + id);

            if(isLocalizationKeyPresent)
            {
                characterName = Localization.GetLocalizedStringInEditor(tableName, prefix + id);
            }

        }


        [ButtonMethod(ButtonMethodDrawOrder.AfterInspector, nameof(isLocalizationKeyPresent), true), UsedImplicitly]
        private void CreateLocalizationKey()
        {
            Localization.CreateKey(tableName, prefix + id);
        }
#endif
    }
}