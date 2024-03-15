using Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Dot : Method
    {
        [SerializeField]
        private SpriteRenderer triangle;

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left, true, true));
            A.SetArrowType(true);

            vectors.Add(chart.GetFreeVector(Vector2.one, true, true));
            B.SetArrowType(true);
            triangle.gameObject.SetActive(true);
        }

        protected override void UpdateMethod()
        {
            var angle = Vector3.Angle(A.Value, B.Value);

            var dot = Vector3.Dot(A.Value, B.Value);
            var normalizedDot = Vector3.Dot(A.Normalized, B.Normalized);

            var a = A.Id.Color(A.Color);
            var b = B.Id.Color(B.Color);
            description = $"Dot product of {A.Name} and {B.Name}: {dot.ToString("0.00")} \n" +
                $"Dot product of normalized {A.Name} and {B.Name}: {normalizedDot.ToString("0.00")}";

            UpdateTriangle(angle, dot);
        }

        private void UpdateTriangle(float angle, float dot)
        {
            triangle.transform.position = Vector3.Project(A.Value, B.Normalized);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            var x = A.Length * cos;
            var perp = Vector2.Perpendicular(triangle.transform.position);
            var perpDir = triangle.transform.position - A.Value;
            var y = Mathf.Sign(Vector3.Dot(perpDir, perp)) * Mathf.Sign(dot) * perpDir.magnitude;

            if (B.Value.x < 0f && B.Value.y == 0f) y *= -1;

            triangle.transform.rotation = B.Rotation * Quaternion.AngleAxis(180f, Vector3.forward);
            triangle.size = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
            triangle.transform.localScale = new Vector2(Mathf.Sign(x), Mathf.Sign(y));
        }
    }
}