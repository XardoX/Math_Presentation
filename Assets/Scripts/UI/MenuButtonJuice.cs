using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace MathPresentation.UI.Juice
{
    public class MenuButtonJuice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Juice Settings")]
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

        [Header("References")]
        [SerializeField]
        private Image image;


        private float startWidth;

        private void Awake()
        {
            Material mat = Instantiate(image.material);

            image.material = mat;
            startWidth = mat.GetFloat("_Width");

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(hoverScale, enterDuration).SetEase(enterEase);
            image.material.DOFloat(startWidth * hoverScale, "_Width", enterDuration)
                .SetEase(enterEase);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, exitDuration).SetEase(exitEase);
            image.material.DOFloat(startWidth, "_Width", exitDuration)
                .SetEase(exitEase);
        }
    }
}