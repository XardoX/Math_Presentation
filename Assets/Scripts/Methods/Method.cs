using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace MathPresentation.Methods
{
    public abstract class Method : MonoBehaviour
    {
        [SerializeField]
        protected string title;

        [SerializeField]
        protected TextMeshProUGUI outputText;

        protected List<MyVector> vectors = new();

        protected ChartController chart;

        public string Title => title;

        public void Init(ChartController chart)
        {
            this.chart = chart;
        }

        protected abstract void SetVectors();

        private void OnEnable()
        {
            vectors.Clear();
            SetVectors();
        }

        private void OnDisable()
        {
            vectors.ForEach(_ => _.Toggle(false));
        }

    }
}