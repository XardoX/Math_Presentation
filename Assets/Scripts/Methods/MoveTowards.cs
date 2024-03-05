using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class MoveTowards : Method
    {
        private MyVector vectorA, vectorB, vectorC;

        [SerializeField]
        private float step = 0.5f;

        private void OnEnable()
        {
            vectorA = chart.GetFreeVector(Vector2.left, true, false);
            vectorB = chart.GetFreeVector(Vector2.one, true, false);
            vectorC = chart.GetFreeVector(false, true, false);
        }

        private void LateUpdate()
        {
            vectorC.Value = Vector3.MoveTowards(vectorA.Value, vectorB.Value, step);
        }
    }
}