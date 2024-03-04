using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : Method
{
    private MyVector vectorA, vectorB, vectorC;

    private void OnEnable()
    {
        vectorA = chart.GetFreeVector(Vector2.left);
        vectorB = chart.GetFreeVector(Vector2.one);
        vectorC = chart.GetFreeVector(false);
    }

    private void OnDisable()
    {
        vectorA.Toggle(false);
        vectorB.Toggle(false);
        vectorC.Toggle(false);
    }

    private void Update()
    {
        vectorC.transform.position = Vector3.Scale(vectorA.Value, vectorB.Value);
    }
}
