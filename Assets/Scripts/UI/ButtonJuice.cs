using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace MathPresentation.UI.Juice
{
    public class ButtonJuice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float hoverScale = 1.1f;

        [SerializeField]
        private float enterDuration = 0.25f;

        [SerializeField]
        private Ease enterEase = Ease.Linear;

        [SerializeField]
        private float exitDuration = 0.25f;

        [SerializeField]
        private Ease exitEase = Ease.Linear;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(hoverScale, enterDuration).SetEase(enterEase);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, exitDuration).SetEase(exitEase);
        }
    }
}