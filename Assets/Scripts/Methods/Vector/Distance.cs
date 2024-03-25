using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Distance : Method
    {
        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left + Vector2.up));
            vectors.Add(chart.GetFreeVector(Vector2.right * 3));
            vectors.Add(chart.GetFreeVector(false, false, true));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            C.Value = Vector3.Lerp(A.Value, B.Value, 0.5f);
        }

        protected override void OnMethodEnable()
        {
            description = $"Calculates distance between {A.Name} and {B.Name}";
        }
    }
}