using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MathPresentation.Toolbox
{
    public class ToolToggle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action onPointerDown, onPointerUp;

        [SerializeField]
        private Toggle toggle;

        [SerializeField]
        private Image fill;

        public bool IsOn => toggle.isOn;

        public UnityEvent<bool> OnValueChanged => toggle.onValueChanged;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp.Invoke();
            StopFill();
        }

        Tween fillTween;
        public void StartFill(float duration)
        {
            if (fill == null) return;
            fillTween?.Kill();
            fillTween = fill.DOFillAmount(1f, duration);
        }

        public void StopFill()
        {
            if (fill == null) return;
            fillTween?.Kill();
            fillTween = fill.DOFillAmount(0f, 0.25f).SetEase(Ease.OutQuint);
        }
    }
}