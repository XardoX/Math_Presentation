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

        [SerializeField]
        private EraserTool eraserTool;

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

        public void OnMethodSwitched(Methods.Method method)
        {
            drawTool.SetCurrentParent(method.transform);
        }

        private void Start()
        {
            toolboxUI.OnToolSelected += SelectTool;
            toolboxUI.OnToolUnselected += UnselectTool;

            toolboxUI.OnToolPointerDown += ToolClickedDown;
            toolboxUI.OnToolPointerUp += ToolClickedUp;
        }

        private void SelectTool(int id)
        {
            if(currentTool != null)
                currentTool.Toggle(false);

            if(id == 6) //todo get rid of hardcode
            {
                eraserTool.Toggle(true);
                currentTool = eraserTool;
            }
            else
            {
                drawTool.Toggle(true);
                drawTool.SetDrawingColor(id);
                currentTool = drawTool;
            }
        }

        private void UnselectTool(int id)
        {
            drawTool.Toggle(toolboxUI.IsAnyToolSelected);
            currentTool.Toggle(false);
            currentTool = null;
        }

        private void ToolClickedDown(int id)
        {
            if (id == 6) //todo get rid of hardcode
            {
                eraserTool.OnToolClickedDown();
                toolboxUI.ToolToggles[id].StartFill(eraserTool.HoldTimeToEraseAll); 
            }
        }


        private void ToolClickedUp(int id)
        {
            if (id == 6) //todo get rid of hardcode
            {
                eraserTool.OnToolClickedUp();
            }
        }
    }
}
