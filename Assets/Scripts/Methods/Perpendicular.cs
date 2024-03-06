using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Perpendicular : Method
    {
        private MyVector vectorA, vectorB;

        private void OnEnable()
        {
            vectorA = chart.GetFreeVector(Vector2.left);
            vectorB = chart.GetFreeVector(Vector2.one);
        }

        private void LateUpdate()
        {
            vectorB.Value = Vector2.Perpendicular(vectorA.Value);
        }
    }
}