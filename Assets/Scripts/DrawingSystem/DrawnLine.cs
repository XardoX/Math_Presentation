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

        private List<Vector2> points;

        public void UpdateLine(Vector2 position)
        {
            if (points == null)
            {
                points = new();
                SetPoint(position);
                return;
            }
            Debug.Log(position);
            if (Vector2.Distance(points.Last(), position) > .1f)
            {
                SetPoint(position);
            }
        }

        private void SetPoint(Vector2 point)
        {
            points.Add(point);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPosition(points.Count -1 , point);
        }
    }
}