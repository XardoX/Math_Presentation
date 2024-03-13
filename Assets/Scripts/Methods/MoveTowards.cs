using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class MoveTowards : Method
    {
        [SerializeField]
        private float step = 0.5f;

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, false));
            vectors.Add(chart.GetFreeVector(Vector2.one, true, false));
            vectors.Add(chart.GetFreeVector(false, false));
        }

        protected override void UpdateMethod()
        {
            vectors[2].Value = Vector3.MoveTowards(vectors[0].Value, vectors[1].Value, step);
        }
    }
}