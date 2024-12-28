using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace MathPresentation.Toolbox
{
    public class ToolboxUI : MonoBehaviour
    {
        public Action<int> OnToolSelected, OnToolUnselected, OnToolPointerDown, OnToolPointerUp;

        [SerializeField]
        private Animator animator;

        private List<ToolToggle> toolToggles;

        private ToolToggle selectedTool;

        private ToggleGroup toggleGroup;

        private bool isToolboxShown = false;

        public bool IsAnyToolSelected => selectedTool != null;

        public List<ToolToggle> ToolToggles => toolToggles;

        public void ToggleToolbox()
        {
            isToolboxShown = !isToolboxShown;
            //animator.SetBool("Show", isToolboxShown); //animation done by Dotween
        }

        private void Awake()
        {
            toolToggles = GetComponentsInChildren<ToolToggle>().ToList();
            toolToggles.ForEach(_ =>
            {
                var id = toolToggles.IndexOf(_);
                _.OnValueChanged.AddListener((value) => OnToolClicked(_));
                _.onPointerDown += () => OnToolPointerDown?.Invoke(id);
                _.onPointerUp += () => OnToolPointerUp?.Invoke(id);
            });
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