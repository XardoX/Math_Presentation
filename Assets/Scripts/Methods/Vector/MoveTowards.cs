using Extensions;
using MathPresentation.LocalizationWrapper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MathPresentation.Methods
{
    public class MoveTowards : Method
    {
        [SerializeField]
        private Color distanceColor;

        private Slider slider;

        private float distance = 0.5f;

        private string distanceString;

        protected override void OnMethodEnable()
        {
            description = Data.DescriptionString.GetLocalizedString(new 
            { 
                A = A.Name, 
                B = B.Name 
            });

            distance = Vector3.Distance(A.Value, B.Value);
            distanceString = Localization.GetVectors("DISTANCE_VALUE");
            slider = chart.View.SetSlider(distance/2, 0f, distance, distanceString.Color(distanceColor));
            slider.onValueChanged.AddListener(SetDistanceValue);
        }

        protected override void OnMethodDisable()
        {
            slider.onValueChanged.RemoveListener(SetDistanceValue);
            chart.View.HideSlider();
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, false));
            vectors.Add(chart.GetFreeVector(Vector2.one * 2, true, false));
            vectors.Add(chart.GetFreeVector(false, false));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            C.Value = Vector3.MoveTowards(A.Value, B.Value, distance);
            chart.View.SetSlider(distance, 0f, Vector3.Distance(A.Value, B.Value), distanceString.Color(distanceColor));
        }

        private void SetDistanceValue(float newDistance)
        {
            distance = newDistance;
            UpdateMethod();
            OnUpdated?.Invoke(this);
        }
    }
}