using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Add : Method
    {
        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left + Vector2.up));
            vectors.Add(chart.GetFreeVector(Vector2.right * 3));
            vectors.Add(chart.GetFreeVector(false, true));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            C.Value = A.Value + B.Value;
        }

        protected override void OnMethodEnable()
        {
            description = $"Adds {A.Name} to {B.Name} component-wise, which results in {C.Name}";
        }
    }
}