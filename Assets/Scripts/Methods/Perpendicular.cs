using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Perpendicular : Method
    {
        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left));
            vectors.Add(chart.GetFreeVector(Vector2.one));
        }

        private void LateUpdate()
        {
            vectors[1].Value = Vector2.Perpendicular(vectors[0].Value);
        }
    }
}