using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Toolbox
{
    public class DrawTool : Tool
    {
        [SerializeField]
        private DrawnLine drawnLinePrefab;

        [SerializeField]
        private Color[] availableColors;

        private Color color;

        private DrawnLine activeLine;

        private List<DrawnLine> drawnLines = new();

        private Camera cam;

        private Transform currentParent;

        private int lineCounter = 0;

        public void SetDrawingColor(int colorId)
        {
            color = availableColors[Mathf.Min(colorId, availableColors.Length-1)];
        }

        public void SetDrawingColor(Color color)
        {
            this.color = color;
        }

        public void ClearAllLines()
        {
            drawnLines.ForEach(_ => Destroy(_ != null ? _.gameObject : null));
            drawnLines.Clear();
        }

        public void ClearAllInCurrentParent()
        {
            var lines = currentParent.GetComponentsInChildren<DrawnLine>();
            foreach (var line in lines)
            {
                drawnLines.Remove(line);
                Destroy(line != null ? line.gameObject : null);
            }
        }

        public void SetCurrentParent(Transform newParent)
        {
            currentParent = newParent;
        }

        public void ResetParent()
        {
            currentParent = transform;
        }

        private void Awake()
        {
            ResetParent();
        }

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (isEnabled == false) return;

            if(Input.GetMouseButtonDown(0))
            {
                activeLine = Instantiate(drawnLinePrefab, currentParent);
                activeLine.name = "Line_" + lineCounter.ToString("00");
                activeLine.SetColor(color);
                drawnLines.Add(activeLine);
                lineCounter++;
            }

            if (Input.GetMouseButtonUp(0) && activeLine != null)
            {

                if (activeLine?.Points.Count < 2) 
                    Destroy(activeLine.gameObject);

                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector3 screenPosDepth = Input.mousePosition;
                screenPosDepth.z = Mathf.Abs(cam.transform.position.z);
                var mousePos = cam.ScreenToWorldPoint(screenPosDepth);
                activeLine.UpdateLine(mousePos);
            }
        }
    }
}