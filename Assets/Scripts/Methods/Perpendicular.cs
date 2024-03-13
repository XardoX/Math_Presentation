using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Perpendicular : Method
    {
        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, true, true));
            vectors.Add(chart.GetFreeVector(Vector2.one, false, true, true));
            vectors[1].TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            vectors[1].Value = Vector2.Perpendicular(vectors[0].Value);
        }
    }
}