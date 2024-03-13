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
    }
}