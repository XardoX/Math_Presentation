using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : Method
{
    [SerializeField]
    private MyVector vectorA, vectorB;

    private void Update()
    {
        var dot = Vector3.Dot(vectorA.Value, vectorB.Value);
        outputText.text = dot.ToString();
    }
}
