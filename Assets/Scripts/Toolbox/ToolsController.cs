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

        private Tool currentTool;

        public void ToggleCurrentTool(bool toggle)
        {
            if (currentTool != null)
            {
                currentTool.Toggle(toggle);
            }
        }

        /// <summary>
        /// Clears blackboard of all tools
        /// </summary>
        public void ClearAll()
        {
            drawTool.ClearAllLines();
        }

        private void Start()
        {
            toolboxUI.OnToolSelected += SelectTool;
            toolboxUI.OnToolUnselected += UnselectTool;

        }

        private void SelectTool(int id)
        {
            drawTool.Toggle(true);
            drawTool.SetDrawingColor(id);
            currentTool = drawTool;
        }

        private void UnselectTool(int id)
        {
            drawTool.Toggle(toolboxUI.IsAnyToolSelected);
            currentTool = null;
        }
    }
}
