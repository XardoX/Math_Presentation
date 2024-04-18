using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace MathPresentation.UI.Tabs
{
    public class TabWindow : MonoBehaviour
    {
        public Action<TabWindow> OnShow, OnHide;

        [Header("Settings")]
        [SerializeField]
        private bool moveX;
        [SerializeField]
        private bool moveY;

        [Header("Juice")]
        [SerializeField]
        private float showDuration = .25f;

        [SerializeField]
        private Ease showEase;

        [SerializeField]
        private float hideDuration = .25f;

        [SerializeField]
        private Ease hideEase;

        [Header("References")]
        [SerializeField]
        private TabButton tabButton;

        private RectTransform rectTransform;

        private Vector2 startPos;

        private bool isShown;

        public void Show()
        {
            var x = moveX ? rectTransform.sizeDelta.x : startPos.x;
            var y = moveY ? rectTransform.sizeDelta.y : startPos.y;
            
            if(moveX)
                x *= rectTransform.pivot.x == 1f ? 1f : -1f;
            if(moveY)
                y *= rectTransform.pivot.y == 1f && moveY ? 1f : -1f;

            var showPos = new Vector2(x, y);
            
            rectTransform.DOAnchorPos(showPos, showDuration).SetEase(showEase);
            OnShow?.Invoke(this);
        }

        public void Hide()
        {
            rectTransform.DOAnchorPos(startPos, hideDuration).SetEase(hideEase);
            OnHide?.Invoke(this);
        }

        public void ShowButton()
        {
            rectTransform.DOAnchorPos(startPos, hideDuration).SetEase(hideEase);
        }

        public void HideCompletly()
        {
            rectTransform.DOAnchorPos(startPos - tabButton.Rect.anchoredPosition, hideDuration).SetEase(hideEase);

        }

        public void Toggle()
        {
            isShown = !isShown;

            if (isShown)
                Show();
            else 
                Hide();
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            startPos = rectTransform.anchoredPosition;

            if(tabButton == null)
                tabButton = GetComponentInChildren<TabButton>(true);
            tabButton.OnClick.AddListener(Toggle);
        }
    }
}