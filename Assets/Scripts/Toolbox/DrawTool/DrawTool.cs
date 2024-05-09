using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UndoDraw();
            }
#endif
            if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            {
                UndoDraw();
            }

            if (isEnabled == false) return;

            if(Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }

            if (Input.GetMouseButtonUp(0) && activeLine != null)
            {
                DeleteActiveLine();
            }else
            {
                DrawActiveLine();
            }

        }

        private void DeleteActiveLine()
        {
            if (activeLine?.Points.Count < 2)
                DeleteLine(activeLine);

            activeLine = null;
        }

        private void DeleteLine(DrawnLine line)
        {
            drawnLines.Remove(line);
            Destroy(line.gameObject);
        }

        private void CreateLine()
        {
            activeLine = Instantiate(drawnLinePrefab, currentParent);
            activeLine.name = "Line_" + lineCounter.ToString("00");
            activeLine.SetColor(color);
            activeLine.OnRightClick += DeleteLine;
            drawnLines.Add(activeLine);
            lineCounter++;
        }

        private void DrawActiveLine()
        {
            if(activeLine != null)
            {
                Vector3 screenPosDepth = Input.mousePosition;
                screenPosDepth.z = Mathf.Abs(cam.transform.position.z);
                var mousePos = cam.ScreenToWorldPoint(screenPosDepth);
                activeLine.UpdateLine(mousePos);
            }
        }

        private void UndoDraw()
        {
            var lineToDelete = drawnLines.LastOrDefault(_ => _.gameObject.activeInHierarchy && _ != activeLine);
            if(lineToDelete != null)
                DeleteLine(lineToDelete);
        }
    }
}