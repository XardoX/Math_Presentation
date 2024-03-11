using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Toolbox
{
    public class ToolsController : MonoBehaviour
    {
        [SerializeField]
        private ToolboxUI toolboxUI;

        [SerializeField]
        private DrawTool drawTool;

        private void Start()
        {
            toolboxUI.OnToolSelected += SelectTool;
            toolboxUI.OnToolUnselected += UnselectTool;

        }

        private void SelectTool(int id)
        {
            drawTool.ToggleDrawing(true);
            drawTool.SetDrawingColor(id);
        }

        private void UnselectTool(int id)
        {
            drawTool.ToggleDrawing(toolboxUI.IsAnyToolSelected);
        }
    }
}
