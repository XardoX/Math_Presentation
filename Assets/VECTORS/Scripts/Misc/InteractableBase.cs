using Ami.BroAudio;
using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MathPresentation
{
    public class InteractableBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Foldout("Juice Settings",true)]
        [SerializeField]
        protected float hoverScale = 1.1f;

        [SerializeField]
        protected float enterDuration = 0.25f;

        [SerializeField]
        protected Ease enterEase = Ease.Linear;

        [SerializeField]
        protected float exitDuration = 0.25f;

        [SerializeField]
        protected Ease exitEase = Ease.Linear;

        [Foldout("Sound", true)]
        [SerializeField]
        protected SoundID onEnterSFX, onExitSFX, onClickSFX;

        [Foldout("References")]
        [SerializeField]
        protected Transform scaleTransform;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClickDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnClickDown();
        }

        protected virtual void OnHoverEnter()
        {
            scaleTransform.DOScale(hoverScale, enterDuration).SetEase(enterEase);
            BroAudio.Play(onEnterSFX);
        }

        protected virtual void OnHoverExit()
        {
            scaleTransform.DOScale(1f, exitDuration).SetEase(exitEase);
            BroAudio.Play(onExitSFX);
        }

        protected virtual void OnClickDown()
        {
            BroAudio.Play(onClickSFX);
        }

        protected virtual void OnClickUp()
        {

        }

        private void Awake()
        {
            if(scaleTransform == null) scaleTransform = transform;
        }
    }
}