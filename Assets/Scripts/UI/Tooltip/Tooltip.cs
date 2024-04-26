using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
namespace MathPresentation.UI.TooltipSystem
{
    public class Tooltip : MonoBehaviour
    {
        public int characterWrapLimit = 30;

        [SerializeField]
        private TextMeshProUGUI headerField;

        [SerializeField]
        private TextMeshProUGUI contentField;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private LayoutElement layoutElement;

        private bool isFree = true;

        public bool IsFree { get => isFree; set => isFree = value; }

        public void SetText(string header, string content)
        {
            headerField.text = header;
            contentField.text = content;

            layoutElement.enabled = (header.Length > characterWrapLimit || content.Length > characterWrapLimit) ? true : false;
        }

        public void Show()
        {
            canvasGroup.DOFade(1f, 0.2f);
        }

        public void Hide()
        {
            canvasGroup.DOFade(0f, 0.2f);
        }
    }
}