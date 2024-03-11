using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MathPresentation.DrawingSystem
{
    public class DrawnLine : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private LineCollider lineCollider;

        private List<Vector2> points;

        public void UpdateLine(Vector2 position)
        {
            if (points == null)
            {
                points = new();
                SetPoint(position);
                return;
            }

            if (Vector2.Distance(points.Last(), position) > .1f)
            {
                SetPoint(position);
            }
            lineCollider.SetEdgeCollider(lineRenderer);
        }

        private void SetPoint(Vector2 point)
        {
            points.Add(point);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count -1 , point);
        }

        private void OnMouseOver()
        {
            if(Input.GetMouseButtonDown(1))
            {
                Destroy(gameObject);
            }
        }
    }
}