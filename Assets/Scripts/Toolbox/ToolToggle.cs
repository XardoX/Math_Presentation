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

        public bool IsOn => toggle.isOn;

        public UnityEvent<bool> OnValueChanged => toggle.onValueChanged;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp.Invoke();
        }
    }
}