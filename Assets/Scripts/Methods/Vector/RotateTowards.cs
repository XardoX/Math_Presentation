using Extensions;
using MathPresentation.LocalizationWrapper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathPresentation.Methods
{
    public class RotateTowards : Method
    {
        [SerializeField]
        private Color angleColor;

        [SerializeField]
        private float maxMagnitudeDelta = 0f;

        private Slider slider;

        private float angleToRotate = 0.5f;

        private string angleKeyword;
        protected override void OnMethodEnable()
        {
            description = Data.DescriptionString.GetLocalizedString(new
            {
                A = A.Name,
                B = B.Name,
                C = C.Name
            });
            angleKeyword = Localization.GetVectors("ROTATETOWARDS_VALUE");

            angleToRotate = Vector3.Angle(A.Value, B.Value);
            slider = chart.View.SetSlider(angleToRotate / 2, 0f, angleToRotate, angleKeyword.Color(angleColor));
            slider.onValueChanged.AddListener(SetAngleValue);
            UpdateMethod();
        }

        protected override void OnMethodDisable()
        {
            slider.onValueChanged.RemoveListener(SetAngleValue);
            chart.View.HideSlider();
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, true));
            vectors.Add(chart.GetFreeVector(Vector2.one + Vector2.right, true, true));
            vectors.Add(chart.GetFreeVector(false, true));
            A.SetArrowType(true);
            B.SetArrowType(true);
            C.SetArrowType(true);
            C.TogglePoint(false);

            angleToRotate = 90f;
        }

        protected override void UpdateMethod()
        {
            C.Value = Vector3.RotateTowards(A.Value, B.Value, angleToRotate * Mathf.Deg2Rad, maxMagnitudeDelta);
            var angle = Vector3.Angle(A.Value, B.Value);
            chart.View.SetSlider(angleToRotate, -360f + angle, angle, angleKeyword.Color(angleColor));
        }

        private void SetAngleValue(float newAngle)
        {
            angleToRotate = newAngle;
            UpdateMethod();
            OnUpdated?.Invoke(this);
        }
    }
}