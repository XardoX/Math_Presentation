using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.DrawingSystem
{
    public class DrawingSystem : MonoBehaviour
    {
        [SerializeField]
        private DrawnLine drawnLinePrefab;

        private DrawnLine activeLine;

        private Camera cam;

        private int lineCounter = 0;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                activeLine = Instantiate(drawnLinePrefab, transform);
                activeLine.name = "Line_" + lineCounter.ToString("00");
                lineCounter++;
            }

            if (Input.GetMouseButtonUp(0))
            {
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