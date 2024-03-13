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