using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Scale : Method
    {
        protected override void OnMethodEnable()
        {
            description = $"Muliplies {A.Name} by {B.Name} components-wise resulting in {C.Name}";
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.up * 2));
            vectors.Add(chart.GetFreeVector(Vector2.right * 2));
            vectors.Add(chart.GetFreeVector(false));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            C.Value = Vector3.Scale(A.Value, B.Value);
        }
    }
}