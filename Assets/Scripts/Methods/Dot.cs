using System.Collections;
using System.Collections.Generic;
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
            vectors[0].SetArrowType(true);

            vectors.Add(chart.GetFreeVector(Vector2.one, true, true));
            vectors[1].SetArrowType(true);
            triangle.gameObject.SetActive(true);
        }

        private void Update()
        {
            var angle = Vector3.Angle(vectors[0].Value, vectors[1].Value);

            var dot = Vector3.Dot(vectors[0].Value, vectors[1].Value);
            outputText.text = dot.ToString("0.00");


            triangle.transform.position = Vector3.Project(vectors[0].Value, vectors[1].Normalized);
            var cos = Mathf.Cos(angle * Mathf.Deg2Rad);
            var x = vectors[0].Length * cos;
            var perp = Vector2.Perpendicular(triangle.transform.position);
            var perpDir = triangle.transform.position - vectors[0].Value;
            var y = Mathf.Sign(Vector3.Dot(perpDir, perp)) * Mathf.Sign(dot) * perpDir.magnitude;

            if (vectors[1].Value.x < 0f && vectors[1].Value.y == 0f) y *= -1;

            triangle.transform.rotation = vectors[1].Rotation * Quaternion.AngleAxis(180f, Vector3.forward);
            triangle.size = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
            triangle.transform.localScale = new Vector2(Mathf.Sign(x), Mathf.Sign(y));
        }

    }
}