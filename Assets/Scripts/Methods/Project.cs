using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Project : Method
    {
        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, true));
            vectors[0].SetArrowType(true);

            vectors.Add(chart.GetFreeVector(Vector2.one, true, true));
            vectors[1].SetArrowType(true);

            vectors.Add(chart.GetFreeVector(false, false, true));
            vectors[2].ToggleArrowPoint(true);
            vectors[2].TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            vectors[2].Value = Vector3.Project(vectors[0].Value, vectors[1].Normalized);
        }
    }
}
