using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace MathPresentation.DialogSystem
{
    public class DialogCloud : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dialogText;
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private static float textSpeed = 2f;

        private const string HTML_ALPHA = "<color=#00000000>";

        private const float kMaxTextTime = 0.1f;

        private bool inProgress = false;

        public bool InProgress = true;
        public void ShowDialogText(string line)
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);

            if (inProgress)
            {
                ShowInstantly(line);
            }
            else
            {
                StartCoroutine(DisplayText(line));
            }
        }
        public void ShowInstantly(string text)
        {
            StopAllCoroutines();
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);

            dialogText.text = text;
        }
        public void Hide()
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, 0.5f);

        }

        private IEnumerator DisplayText(string text)
        {
            inProgress = true;
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

            inProgress = false;
        }
    }
}