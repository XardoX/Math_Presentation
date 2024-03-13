using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MathPresentation.Methods
{
    public class Lerp : Method
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private SpriteRenderer angleCircle;

        [SerializeField]
        private bool unclampedLerp;

        [SerializeField]
        private bool isSlerp;

        [SerializeField]
        private bool fixSlerpCenter;

        private float t = 0.5f;

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, false));
            vectors.Add(chart.GetFreeVector(Vector2.one, true, false));
            vectors.Add(chart.GetFreeVector(false, false));
        }

        protected override void UpdateMethod()
        {
            if (isSlerp)
                HandleSlerp();
            else
                HandleLerp();
        }

        private void Start()
        {
            slider.onValueChanged.AddListener(SetLerpValue);
            description = t.ToString("0.00");
        }

        private void HandleLerp()
        {
            if (unclampedLerp)
            {
                vectors[2].Value = Vector2.LerpUnclamped(vectors[0].Value, vectors[1].Value, t);

            }
            else
            {
                vectors[2].Value = Vector2.Lerp(vectors[0].Value, vectors[1].Value, t);
            }
        }

        private void HandleSlerp()
        {
            Vector3 center = Vector3.zero;
            if (fixSlerpCenter)
            {
                center = (vectors[0].Value + vectors[1].Value) * 0.5F;
                //center -= new Vector3(0, 1, 0);
            }

            angleCircle.transform.position = center;

            var distance = Vector3.Distance(vectors[0].Value, vectors[1].Value);

            angleCircle.transform.localScale = new Vector3(distance, distance, angleCircle.transform.localScale.z);

            Vector3 relCenterA = vectors[0].Value - center;
            Vector3 relCenterB = vectors[1].Value - center;

            if (unclampedLerp)
            {
                vectors[2].Value = Vector3.SlerpUnclamped(relCenterA, relCenterB, t);
            }
            else
            {
                vectors[2].Value = Vector3.Slerp(relCenterA, relCenterB, t);
            }

            vectors[2].Value += center;
        }

        private void SetLerpValue(float t)
        {
            this.t = t;
            description = t.ToString("0.00");
        }
    }
}