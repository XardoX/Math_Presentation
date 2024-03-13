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
            vectors.Add(chart.GetFreeVector(Vector2.left));
            vectors.Add(chart.GetFreeVector(Vector2.right));
            vectors.Add(chart.GetFreeVector(false, true));
        }

        protected override void UpdateMethod()
        {
            vectors[2].Value = vectors[0].Value + vectors[1].Value;
        }

        protected override void OnMethodEnable()
        {
            var a = vectors[0].Id.Color(vectors[0].Color);
            var b = vectors[1].Id.Color(vectors[1].Color);
            var c = vectors[2].Id.Color(vectors[2].Color);
            description = $"Adds {a} to {b} component-wise which results in {c}";
        }
    }
}