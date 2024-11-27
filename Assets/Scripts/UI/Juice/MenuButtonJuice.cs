using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using MyBox;

namespace MathPresentation.UI.Juice
{
    public class MenuButtonJuice : InteractableBase
    {
        [Foldout("References")]
        [SerializeField]
        private Image image;

        private float startWidth;

        private void Awake()
        {
            Material mat = Instantiate(image.material);

            image.material = mat;
            startWidth = mat.GetFloat("_Width");
        }

        protected override void OnHoverEnter()
        {
            base.OnHoverEnter();
            image.material.DOFloat(startWidth * hoverScale, "_Width", enterDuration)
                .SetEase(enterEase);
        }

        protected override void OnHoverExit()
        {
            base.OnHoverExit();
            image.material.DOFloat(startWidth, "_Width", exitDuration)
                .SetEase(exitEase);
        }
    }
}