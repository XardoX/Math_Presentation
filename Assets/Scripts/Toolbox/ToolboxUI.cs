using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MathPresentation.Toolbox
{
    public class ToolboxUI : MonoBehaviour
    {
        public Action<int> OnToolSelected, OnToolUnselected;

        [SerializeField]
        private Animator animator;

        private List<ToolToggle> toolToggles;

        private ToolToggle selectedTool;

        private ToggleGroup toggleGroup;

        private bool isToolboxShown = false;

        public bool IsAnyToolSelected => selectedTool != null;

        public void ToggleToolbox()
        {
            isToolboxShown = !isToolboxShown;
            //animator.SetBool("Show", isToolboxShown);
        }

        private void Awake()
        {
            toolToggles = GetComponentsInChildren<ToolToggle>().ToList();
            toolToggles.ForEach(_ => _.OnValueChanged.AddListener((value) => OnToolClicked(_)));
        }

        private void OnToolClicked(ToolToggle toggle)
        {
            var id = toolToggles.IndexOf(toggle);
            if(toggle.IsOn)
            {
                selectedTool = toggle;
                OnToolSelected?.Invoke(id);
            }
            else
            {
                selectedTool = null;
                OnToolUnselected?.Invoke(id);
            }
        }
    }
}