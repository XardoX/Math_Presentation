using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : Method
{
    [SerializeField]
    private MyVector vectorA, vectorB;

    private void LateUpdate()
    {
        var angle = Vector3.Angle(vectorA.transform.position, vectorB.transform.position);
        angle = Mathf.Round(angle);
        outputText.text = angle.ToString();
    }
}
