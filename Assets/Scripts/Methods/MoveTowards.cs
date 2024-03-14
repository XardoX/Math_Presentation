using Extensions;
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

        protected override void OnMethodEnable()
        {
            description = $"Calculates a position between {A.Name} and {B.Name} using";

            distance = Vector3.Distance(A.Value, B.Value);
            slider = chart.View.SetSlider(distance/2, 0f, distance, "distance: ".Color(distanceColor));
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
            vectors.Add(chart.GetFreeVector(Vector2.one, true, false));
            vectors.Add(chart.GetFreeVector(false, false));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            C.Value = Vector3.MoveTowards(A.Value, B.Value, distance);
            chart.View.SetSlider(distance, 0f, Vector3.Distance(A.Value, B.Value), "distance: ".Color(distanceColor));
        }

        private void SetDistanceValue(float newDistance)
        {
            distance = newDistance;
            UpdateMethod();
            OnUpdated?.Invoke(this);
        }
    }
}