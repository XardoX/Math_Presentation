using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Add : Method
    {
        private MyVector vectorA, vectorB, vectorC;

        private void OnEnable()
        {
            vectorA = chart.GetFreeVector(Vector2.left);
            vectorB = chart.GetFreeVector(Vector2.right);
            vectorC = chart.GetFreeVector(false, true);
        }
        private void OnDisable()
        {
            vectorA.Toggle(false);
            vectorB.Toggle(false);
            vectorC.Toggle(false);
        }

        private void LateUpdate()
        {
            vectorC.Value = vectorA.Value + vectorB.Value; 
        }
    }
}