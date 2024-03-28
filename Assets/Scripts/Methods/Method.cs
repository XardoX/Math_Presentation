using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MathPresentation.Methods
{
    public abstract class Method : MonoBehaviour
    {
        public Action<Method> OnEnabled, 
            OnUpdated, 
            OnDisabled;

        [SerializeField]
        private MethodData data;

        [SerializeField]
        protected string title;

        [SerializeField]
        private bool doubleChart = false;

        protected string description;

        protected List<MyVector> vectors = new();

        protected ChartController chart;

        public string Title => title;

        public string Description => description;

        protected MyVector A => vectors[0];
        protected MyVector B => vectors[1];
        protected MyVector C => vectors[2];

        public MethodData Data => data;

        public List<MyVector> Vectors => vectors;

        public void Init(ChartController chart)
        {
            this.chart = chart;
        }

        protected abstract void SetVectors();

        protected abstract void UpdateMethod();

        protected virtual void OnMethodEnable()
        {

        }

        protected virtual void OnVectorsUpdate()
        {
            UpdateMethod();
            vectors.ForEach(v => v.UpdateVector(true));
            OnUpdated?.Invoke(this);
        }

        protected virtual void OnMethodDisable()
        {

        }

        private void OnEnable()
        {
            chart.ToggleDoubleChart(doubleChart);
            vectors.Clear();
            SetVectors();
            vectors.ForEach(v => v.OnUpdated += OnVectorsUpdate);
            OnMethodEnable();
            OnEnabled?.Invoke(this);
            UpdateMethod();
            vectors.ForEach(v => v.UpdateVector(true));
            OnUpdated?.Invoke(this);
        }

        private void OnDisable()
        {
            vectors.ForEach(v => v.OnUpdated -= OnVectorsUpdate);
            vectors.ForEach(_ => _.Toggle(false));
            vectors.Clear();
            OnMethodDisable();
            OnDisabled?.Invoke(this);
        }

    }
}