using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Dot : Method
    {
        [SerializeField]
        private SpriteRenderer triangle;

        private MyVector vectorA, vectorB;

        private void OnEnable()
        {
            vectorA = chart.GetFreeVector(Vector2.left, true, true);
            vectorA.SetArrowType(true);
            vectorB = chart.GetFreeVector(Vector2.one, true, true);
            vectorB.SetArrowType(true);
            triangle.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            vectorA.Toggle(false);
            vectorB.Toggle(false);
            triangle.gameObject.SetActive(false);
        }

        private void Update()
        {
            var angle = Vector3.Angle(vectorA.Value, vectorB.Value);

            var dot = Vector3.Dot(vectorA.Value, vectorB.Value);
            outputText.text = dot.ToString("0.00");


            triangle.transform.position = Vector3.Project(vectorA.Value, vectorB.Normalized);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            var x = vectorA.Length * cos;
            var perp = Vector2.Perpendicular(triangle.transform.position);
            var perpDir = triangle.transform.position - vectorA.Value;
            var y = Mathf.Sign(Vector3.Dot(perpDir, perp)) * Mathf.Sign(dot) * perpDir.magnitude;

            if (vectorB.Value.x < 0f && vectorB.Value.y == 0f) y *= -1;

            triangle.transform.rotation = vectorB.Rotation * Quaternion.AngleAxis(180f, Vector3.forward);
            triangle.size = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
            triangle.transform.localScale = new Vector2(Mathf.Sign(x), Mathf.Sign(y));
        }

    }
}