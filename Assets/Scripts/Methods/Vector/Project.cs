using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Project : Method
    {
        protected override void OnMethodEnable()
        {
            description = $"Projects {A.Name} on to {B.Name} which results in {C.Name}";
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.one + Vector2.right, true, true));
            A.SetArrowType(true);

            vectors.Add(chart.GetFreeVector(Vector2.left * 2, true, true));
            B.SetArrowType(true);

            vectors.Add(chart.GetFreeVector(false, false, true));
            C.ToggleArrowPoint(true);
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            C.Value = Vector3.Project(A.Value, B.Normalized);
        }
    }
}
