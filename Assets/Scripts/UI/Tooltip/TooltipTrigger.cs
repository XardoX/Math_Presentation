using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MathPresentation.UI.TooltipSystem
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private string tooltipHeader;

        [SerializeField]
        [TextArea(2, 10)]
        private string tooltipContent;

        private Tooltip tooltip;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(tooltip == null)
            {
                tooltip = TooltipsManager.GetTooltip();
            }
            tooltip.SetText(tooltipHeader, tooltipContent);
            tooltip.Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(tooltip != null)
            {
                tooltip.Hide();
                tooltip = null;
            }
        }

        public void OverrideText(string header = "", string content = "")
        {
            tooltip.SetText(header, content);
        }
    }
}