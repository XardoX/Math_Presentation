using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Extensions;
using DG.Tweening;
using MathPresentation.UI;
using System;

namespace MathPresentation.DialogSystem
{
    public class DialogUI : Window
    {
        public Action onDialogEnded;

        [SerializeField]
        private Image characterGraphicLeft, 
            characterGraphicRight;

        [SerializeField]
        private TextMeshProUGUI dialogText, 
            characterNameLeft, 
            characterNameRight;

        [SerializeField]
        private float textSpeed = 2f;

        [SerializeField]
        private AudioSource leftCharacterSource, 
            rightCharacaterSource;

        private Sprite leftGraphic, 
            rightGraphic;

        bool isTyping;

        private const string HTML_ALPHA = "<color=#00000000>";

        private const float kMaxTextTime = 0.1f;

        public bool IsTyping => isTyping;


        public void StartDialog(DialogData data)
        {
            if (data == null)
            {
                Debug.LogError("DialogData is null. Cannot start dialog.");
                return;
            }

            data.LoadLines();


            if (data.Dialog == null || data.Dialog.Count == 0)
            {
                Debug.LogError("Dialog is null or empty. Cannot start dialog.");
                return;
            }

            var firstLine = data.Dialog[0];
            if (firstLine == null)
            {
                Debug.LogError("First dialog entry is null. Cannot process dialog.");
                return;
            }

            SetCharacters(firstLine);

            Toggle(true);

            SetAudioSources(data);

            PlayNextLine(firstLine);
        }

       

        public void PlayNextLine(DialogLine line)
        {
            if(leftCharacterSource != null) 
                leftCharacterSource.Stop();

            if (rightCharacaterSource != null) 
                rightCharacaterSource.Stop();

            if (isTyping)
            {
                DisplayInstantly(line.Line);
            }
            else
            {
                StartCoroutine(DisplayText(line.Line));
                if (line.SpeakingCharacterId == 0)
                {
                    if (leftCharacterSource != null)
                        leftCharacterSource.Play();

                    HighlightSpeakingCharacter(true);

                }
                else
                {
                    if (rightCharacaterSource != null)
                        rightCharacaterSource.Play();

                    HighlightSpeakingCharacter(false);

                }

                if(characterGraphicLeft != null)
                    characterGraphicLeft.sprite = line.OverrideGraphicLeft != null ? line.OverrideGraphicLeft : leftGraphic;

                if (characterGraphicRight != null)
                    characterGraphicRight.sprite = line.OverrideGraphicRight != null ? line.OverrideGraphicRight : rightGraphic;
            }
        }

        public void EndDialog()
        {
            Toggle(false);

            isTyping = false;

            Time.timeScale = 1f;

            if (leftCharacterSource != null)
                leftCharacterSource.Stop()
                    ;
            if (rightCharacaterSource != null)
                rightCharacaterSource.Stop();

            onDialogEnded?.Invoke();
        }

        public void DisplayInstantly(string text)
        {
            StopAllCoroutines();
            dialogText.text = text;
            isTyping = false;

            if (leftCharacterSource != null)
                leftCharacterSource.Stop();

            if (rightCharacaterSource != null)
                rightCharacaterSource.Stop();
        }

        private void SetCharacters(DialogLine line)
        {
            // Handle left characters
            if (line.CharactersLeft != null && line.CharactersLeft.Length > 0 && line.CharactersLeft[0] != null)
            {
                leftGraphic = line.CharactersLeft[0]?.CharacterGraphic;
                characterNameLeft.text = line.CharactersLeft[0]?.CharacterName ?? string.Empty;
                characterGraphicLeft.sprite = leftGraphic;
            }
            else Debug.LogWarning("Left characters are null, empty, or invalid.");


            // Handle right characters
            if (line.CharactersRight != null && line.CharactersRight.Length > 0 && line.CharactersRight[0] != null)
            {
                rightGraphic = line.CharactersRight[0]?.CharacterGraphic;
                characterNameRight.text = line.CharactersRight[0]?.CharacterName ?? string.Empty;
                characterGraphicRight.sprite = rightGraphic;
            }
            else Debug.LogWarning("Right characters are null, empty, or invalid.");
        }

        private void SetAudioSources(DialogData data)
        {
            if (data.Characters != null && data.Characters.Length > 0 && leftCharacterSource != null && rightCharacaterSource != null)
            {
                leftCharacterSource.clip = data.Characters[0]?.MouthSounds;

                if (data.Characters.Length > 1)
                {
                    rightCharacaterSource.clip = data.Characters[1]?.MouthSounds;
                }
                else Debug.LogWarning("Right character audio source is missing or invalid.");
            }
            else Debug.LogWarning("Characters data is null or empty. Audio sources not assigned.");

        }

        private IEnumerator DisplayText(string text)
        {
            isTyping = true;
            dialogText.text = "";
            int alphaInex = 0;
            string originalText = text;
            string displayedText = "";
            foreach (var c in text.ToCharArray())
            {
                alphaInex++;
                dialogText.text = originalText;
                displayedText = dialogText.text.Insert(alphaInex, HTML_ALPHA);
                dialogText.text = displayedText;

                yield return new WaitForSecondsRealtime(kMaxTextTime / textSpeed);
            }

            if (leftCharacterSource != null)
                leftCharacterSource.Stop();

            if (rightCharacaterSource != null)
                rightCharacaterSource.Stop();

            isTyping = false;
        }

        private void HighlightSpeakingCharacter(bool left)
        {
            if (characterGraphicLeft == null || characterGraphicRight == null) return;

            if (left)
            {
                characterGraphicLeft.DOColor(Color.white, 0.25f).SetUpdate(true);
                characterGraphicRight.DOColor(Color.gray, 0.25f).SetUpdate(true);
                characterGraphicLeft.transform.DOScale(1.1f, 0.5f).SetUpdate(true);
                characterGraphicRight.transform.DOScale(1f, 0.25f).SetUpdate(true);
            }
            else
            {
                characterGraphicLeft.DOColor(Color.gray, 0.25f).SetUpdate(true);
                characterGraphicRight.DOColor(Color.white, 0.25f).SetUpdate(true);
                characterGraphicRight.transform.DOScale(1.1f, 0.5f).SetUpdate(true);
                characterGraphicLeft.transform.DOScale(1f, 0.25f).SetUpdate(true);
            }
        }
    }
}