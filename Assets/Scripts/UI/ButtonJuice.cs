using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Ami.BroAudio;
using UnityEngine.UI;

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

        [SerializeField]
        private SoundID onEnterSFX, onExitSFX, onClickSFX;

        private Button button;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(hoverScale, enterDuration).SetEase(enterEase);
            BroAudio.Play(onEnterSFX);

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, exitDuration).SetEase(exitEase);
            BroAudio.Play(onExitSFX);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            BroAudio.Play(onClickSFX);
        }
    }
}