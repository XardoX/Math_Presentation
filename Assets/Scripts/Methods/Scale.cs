using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Scale : Method
    {
        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left));
            vectors.Add(chart.GetFreeVector(Vector2.one));
            vectors.Add(chart.GetFreeVector(false));
        }

        private void Update()
        {
            vectors[2].Value = Vector3.Scale(vectors[0].Value, vectors[1].Value);
        }
    }
}