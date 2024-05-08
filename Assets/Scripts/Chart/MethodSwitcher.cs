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
        public Action<Method> OnSwitched;

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
        private ChalkMask chalkMask;

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
            if (id < 0) return;
            SwitchMethods(id, hidePos);

            chalkMask.Play();
        }

        public void ShowNextMethod()
        {
            var id = activeMethodId + 1;
            if (id > methods.Length - 1) return;
            SwitchMethods(id, showPos);
            chalkMask.PlayReverse();
        }

        public void ToggleMethod(int id, bool toggle)
        {
            if (toggle)
            {
                activeMethodId = id;
                var method = methods[id];
                tween = chartParent.DOMove(Vector3.zero, showduration)
                    .SetEase(showEase)
                    .OnComplete(() =>
                    {
                        OnSwitched?.Invoke(method);
                        method.gameObject.SetActive(true);
                    });
            }
            else
            {
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

            ToggleMethodButtons();

            tween?.Kill();

            tween = chartParent.DOMove(-pos, hideDuration)
                .SetEase(hideEase)
                .OnComplete(() =>
                {
                    chartParent.transform.position = pos;
                    ToggleMethod(activeMethodId, true);
                });
        }

        private void ToggleMethodButtons()
        {
            if (activeMethodId <= 0)
            {
                chartUI.TogglePreviousButton(false);
            }
            else if (activeMethodId >= methods.Length - 1)
            {
                chartUI.ToggleNextButton(false);
            }
        }

        private void Awake()
        {
            methods = FindObjectsOfType<Method>(true);
        }

        private void Start()
        {
            ToggleMethodButtons();
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