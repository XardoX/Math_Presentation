using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation
{
    public class Project : Method
    {
        private MyVector vectorA, vectorB, vectorC;

        private void OnEnable()
        {
            vectorA = chart.GetFreeVector(Vector2.left, true, false);
            vectorB = chart.GetFreeVector(Vector2.one, true, false);
            vectorC = chart.GetFreeVector(false, true, false, true);
        }

        private void LateUpdate()
        {
            vectorC.Value = Vector3.Project(vectorA.Value, vectorB.Normalized);
        }
    }
}
