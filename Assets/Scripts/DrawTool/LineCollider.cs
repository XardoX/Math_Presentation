using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class LineCollider : MonoBehaviour
    {
        [SerializeField]
        private bool autoUpdate = false;

        [Header("References")]
        [SerializeField]
        private EdgeCollider2D edgeCollider;

        [SerializeField]
        private LineRenderer lineRenderer;

        public void SetEdgeCollider() => SetEdgeCollider(lineRenderer);
        
        public void SetEdgeCollider(LineRenderer lineRenderer)
        {
            var edges = new List<Vector2>();

            for (var i = 0; i < lineRenderer.positionCount; i++)
            {
                var point = lineRenderer.GetPosition(i);
                edges.Add(point);
            }

            edgeCollider.SetPoints(edges);
        }

        private void Update()
        {
            if (autoUpdate)
            {
                SetEdgeCollider();
            }
        }
    }
}