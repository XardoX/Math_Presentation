using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Perpendicular : Method
    {
        protected override void OnMethodEnable()
        {
            description = $"Returns the 2D vector {B.Name} perpendicular to vector {A.Name}";
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left * 2, true, true, true));
            vectors.Add(chart.GetFreeVector(Vector2.one, false, true, true));
            B.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            B.Value = Vector2.Perpendicular(A.Value);
        }
    }
}