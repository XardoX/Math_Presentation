using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MathPresentation.Methods
{
    public class Lerp : Method
    {
        [Header("Settings")]
        [SerializeField]
        private Color tColor = Color.yellow;

        [SerializeField]
        private bool unclampedLerp;

        [SerializeField]
        private bool isSlerp;

        [SerializeField]
        private bool fixSlerpCenter;

        [Header("References")]
        [SerializeField]
        private SpriteRenderer angleCircle;

        private Slider slider;

        private float t = 0.5f;

        protected override void OnMethodEnable()
        {
            description = $"Lineary interpolates between {A.Name} and {B.Name} by the {"t".Color(tColor)} resulting in {C.Name}";
            t = 0.5f;
            slider = chart.View.SetSlider(t, 0f, 1f, "t: ".Color(tColor));
            slider.onValueChanged.AddListener(SetLerpValue);
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
            if (isSlerp)
                HandleSlerp();
            else
                HandleLerp();
        }
       
        protected override void OnMethodDisable()
        {
            slider.onValueChanged.RemoveListener(SetLerpValue);
            chart.View.HideSlider();
        }

        private void HandleLerp()
        {
            if (unclampedLerp)
            {
                C.Value = Vector2.LerpUnclamped(A.Value, B.Value, t);

            }
            else
            {
                C.Value = Vector2.Lerp(A.Value, B.Value, t);
            }
        }

        private void HandleSlerp()
        {
            Vector3 center = Vector3.zero;
            if (fixSlerpCenter)
            {
                center = (A.Value + B.Value) * 0.5F;
                //center -= new Vector3(0, 1, 0);
            }

            angleCircle.transform.position = center;

            var distance = Vector3.Distance(A.Value, B.Value);

            angleCircle.transform.localScale = new Vector3(distance, distance, angleCircle.transform.localScale.z);

            Vector3 relCenterA = A.Value - center;
            Vector3 relCenterB = B.Value - center;

            if (unclampedLerp)
            {
                C.Value = Vector3.SlerpUnclamped(relCenterA, relCenterB, t);
            }
            else
            {
                C.Value = Vector3.Slerp(relCenterA, relCenterB, t);
            }

            C.Value += center;
        }

        private void SetLerpValue(float t)
        {
            this.t = t;
            UpdateMethod();
            OnUpdated?.Invoke(this);
        }
    }
}