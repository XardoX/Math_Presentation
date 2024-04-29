using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MathPresentation.UI.TooltipSystem
{
    public class TooltipsManager : MonoBehaviour
    {
        public static TooltipsManager Instance;

        [SerializeField]
        private Tooltip tooltipPrefab;

        private List<Tooltip> tooltips = new();

        public static Tooltip GetTooltip()
        {
            var tooltip = Instance.tooltips.FirstOrDefault(tooltip => tooltip.IsFree);
            if(tooltip == null)
            {
                tooltip = Instantiate(Instance.tooltipPrefab, Instance.transform);
                Instance.tooltips.Add(tooltip);
            }
            tooltip.IsFree = false;
            return tooltip;
        }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            } else
            {
                Destroy(gameObject);
            }
        }
    }
}