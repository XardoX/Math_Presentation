using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MathPresentation.Toolbox
{
    public class DrawnLine : MonoBehaviour
    {
        public Action<DrawnLine> OnRightClick;

        [SerializeField]
        private LineRenderer lineRenderer;

        [SerializeField]
        private LineCollider lineCollider;

        private List<Vector2> points;

        public List<Vector2> Points => points; 

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

        public void SetColor(Color color)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
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
                OnRightClick?.Invoke(this);
            }
        }
    }
}