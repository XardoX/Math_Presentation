using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Distance : Method
    {
        [SerializeField]
        private Line line;

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left + Vector2.up));
            vectors.Add(chart.GetFreeVector(Vector2.right * 3));
        }

        protected override void UpdateMethod()
        {
            line.transform.position = Vector3.Lerp(A.Value, B.Value, 0.5f);
            var target = Quaternion.Euler(0, 0, 90) * (A.Value - B.Value);
            line.transform.rotation = Quaternion.LookRotation(Vector3.forward,target );

            var distance = Vector3.Distance(A.Value, B.Value);
            line.SetLength(distance);
        }

        protected override void OnMethodEnable()
        {
            description = $"Calculates distance between {A.Name} and {B.Name}";
        }
    }
}