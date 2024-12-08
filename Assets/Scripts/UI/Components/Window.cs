using DG.Tweening;
using MyBox;
using System;
using UnityEngine;

namespace MathPresentation.UI
{
    public class Window : MonoBehaviour
    {
        public Action<Window> OnShow, OnHide;

        [SerializeField]
        [Foldout("References")]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private float tweenDuration = 0.2f;

        [SerializeField]
        private Ease showEase, hideEase;

        public void Show()
        {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, tweenDuration).SetEase(showEase);
            OnShow?.Invoke(this);
        }

        public void Hide(bool withoutNotify = false)
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0f, tweenDuration)
                .SetEase(hideEase)
                .OnComplete(() => gameObject.SetActive(false));
            if(!withoutNotify)
                OnHide?.Invoke(this);
        }



        public void Toggle(bool toggle)
        {
            if (toggle)
            {
                Show();
            }
            else Hide();
        }


        private void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

    }
}