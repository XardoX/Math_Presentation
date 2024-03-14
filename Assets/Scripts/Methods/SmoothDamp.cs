using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MathPresentation.Methods
{
    public class SmoothDamp : Method
    {
        [SerializeField]
        private Color timeColor = Color.yellow;

        private Slider slider;

        private Vector3 velocity;

        private float smoothTime = 0.5f;

        protected override void OnMethodEnable()
        {
            description = $"Gradually changes a vector towards a desired goal over time.";

            smoothTime = .5f;
            slider = chart.View.SetSlider(smoothTime, 0f, 1f, "time: ".Color(timeColor));
            slider.onValueChanged.AddListener(SetTimeValue);
            UpdateMethod();
        }

        protected override void OnMethodDisable()
        {
            slider.onValueChanged.RemoveListener(SetTimeValue);
            chart.View.HideSlider();
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, false));
            vectors.Add(chart.GetFreeVector(Vector2.one, false, false));
            B.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            B.Value = Vector3.SmoothDamp(B.Value, A.Value, ref velocity, smoothTime);
        }
        private void Update()
        { 
        }

        private void SetTimeValue(float newTime)
        {
            smoothTime = newTime;
            UpdateMethod();
            OnUpdated?.Invoke(this);
        }
    }
}