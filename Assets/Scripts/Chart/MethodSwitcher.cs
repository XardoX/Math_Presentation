using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MathPresentation.Methods;
using MathPresentation.Toolbox;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using System;

namespace MathPresentation
{
    public class MethodSwitcher : MonoBehaviour
    {
        public Action OnSwitched;

        [Header("Settings")]
        [SerializeField]
        private Vector2 hidePos;

        [SerializeField] 
        private Vector2 showPos;

        [SerializeField]
        private float showduration = 0.2f, hideDuration = 0.2f;

        [SerializeField]
        private Ease showEase, hideEase;

        [Header("References")]
        [SerializeField]
        private Transform chartParent;

        [SerializeField]
        private MaskController maskController;

        private Method[] methods;

        private ChartUI chartUI;

        private int activeMethodId;

        private Tween tween;

        public void Init(ChartUI chartUI)
        { 
            this.chartUI = chartUI;
        }

        public void ShowPreviousMethod()
        {
            var id = activeMethodId - 1;
            SwitchMethods(id, hidePos);
        }

        public void ShowNextMethod()
        {
            var id = activeMethodId + 1;
            SwitchMethods(id, showPos);
        }

        public void ToggleMethod(int id, bool toggle)
        {
            if (toggle)
            {
                activeMethodId = id;
                tween = chartParent.DOMove(Vector3.zero, showduration)
                    .SetEase(showEase)
                    .OnComplete(() =>
                    {
                        OnSwitched?.Invoke();
                        methods[id].gameObject.SetActive(true);
                    });
            }
            else
            {
                maskController.Play();
                methods[id].gameObject.SetActive(false);
                activeMethodId = -1;
            }
        }

        private void SwitchMethods(int id, Vector2 pos)
        {
            chartUI.TogglePreviousButton(true);
            chartUI.ToggleNextButton(true);

            ToggleMethod(activeMethodId, false);
            activeMethodId = Mathf.Clamp(id, 0, methods.Length - 1);

            if (activeMethodId <= 0)
            {
                chartUI.TogglePreviousButton(false);
            } 
            else if(activeMethodId >= methods.Length -1)
            {
                chartUI.ToggleNextButton(false);
            }

            tween?.Kill();

            tween = chartParent.DOMove(-pos, hideDuration)
                .SetEase(hideEase)
                .OnComplete(() =>
                {
                    chartParent.transform.position = pos;
                    ToggleMethod(activeMethodId, true);
                });
        }

        private void Awake()
        {
            methods = FindObjectsOfType<Method>(true);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShowPreviousMethod();
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShowNextMethod();
            }
        }
    }
}