using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : Method
{
    [SerializeField]
    private MyVector vectorA, vectorB;

    [SerializeField]
    private SpriteRenderer triangle;

    private void Update()
    {
        var angle = Vector3.Angle(vectorA.Value, vectorB.Value);

        var dot = Vector3.Dot(vectorA.Value, vectorB.Value);
        outputText.text = dot.ToString();


        triangle.transform.position = Vector3.Project(vectorA.Value, vectorB.Normalized);
        var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        var x = vectorA.Length * cos;
        var perp = Vector2.Perpendicular(triangle.transform.position);
        var perpDir = triangle.transform.position - vectorA.Value;
        var y =  Mathf.Sign(Vector3.Dot(perpDir, perp)) * Mathf.Sign(dot) * perpDir.magnitude;

        if(vectorB.Value.x < 0f && vectorB.Value.y == 0f) y *=-1;
        
        triangle.transform.rotation = vectorB.Rotation * Quaternion.AngleAxis(180f, Vector3.forward);
        triangle.size = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
        triangle.transform.localScale = new Vector2(Mathf.Sign(x), Mathf.Sign(y));
    }

}
