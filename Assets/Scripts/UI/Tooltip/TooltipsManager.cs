using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.UI.TooltipSystem
{
    public class TooltipsManager : MonoBehaviour
    {
        public static TooltipsManager Instance;

        [SerializeField]
        private Tooltip tooltipPrefab;

        private List<Tooltip> tooltips;

        public Tooltip GetTooltip()
        {
            return null;
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