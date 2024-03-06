using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace MathPresentation.Methods
{
    public abstract class Method : MonoBehaviour
    {
        public Action<Method> OnEnabled, OnDisabled;

        [SerializeField]
        protected string title;

        [SerializeField]
        protected string description;

        protected List<MyVector> vectors = new();

        protected ChartController chart;

        public string Title => title;

        public string Description => description;

        public void Init(ChartController chart)
        {
            this.chart = chart;
        }

        protected abstract void SetVectors();

        protected virtual void OnMethodEnable()
        {

        }

        protected virtual void OnMethodDisable()
        {

        }

        private void OnEnable()
        {
            vectors.Clear();
            SetVectors();
            OnMethodEnable();
            OnEnabled?.Invoke(this);
        }

        private void OnDisable()
        {
            vectors.ForEach(_ => _.Toggle(false));
            vectors.Clear();
            OnMethodDisable();
            OnDisabled?.Invoke(this);
        }

    }
}